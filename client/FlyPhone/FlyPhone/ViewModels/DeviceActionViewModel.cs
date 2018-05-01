using System;
using System.Threading;
using FlyClientApi;
using FlyClientApi.Enums;
using Plugin.Toasts;
using Xamarin.Forms;

namespace FlyPhone.ViewModels
{
    public class DeviceActionViewModel : BaseViewModel
    {
        private Command _shutdownButtonCommand;
        private Command _restartButtonCommand;
        private Command _sleepButtonCommand;
        private Command _muteButtonCommand;

        private bool _isEnableShutdownButton;
        private bool _isEnableRestartButton;
        private bool _isEnableSleepButton;
        private bool _isEnableMuteButton;

        private bool _isDeviceActive;
        private string _status;
        private string _name;
        private readonly string _deviceId;

        public Toggle ToggleBlocks { get; } = new Toggle();
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private bool IsDeviceActive
        {
            get => _isDeviceActive;
            set
            {
                _isDeviceActive = value;
                OnPropertyChanged(nameof(DeviceStatusText));
                OnPropertyChanged(nameof(DeviceStatusColor));
            }
        }

        public String DeviceStatusText => IsDeviceActive ? "Active" : "Inactive";

        public Color DeviceStatusColor => IsDeviceActive ? Color.Green : Color.Red;

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public DeviceActionViewModel(string deviceId)
        {
            _deviceId = deviceId;
            new Thread(GetDeviceAndEditButtonsState).Start();
        }

        private async void GetDeviceAndEditButtonsState()
        {
            Status = "Updating displayed device";
            ToggleBlocks.ActivityIndicator = true;

            Models.Device device;
            try
            {
                device = await RequestHandler.DoRequest(Client.GetDevice(_deviceId));
            }
            catch (PhoneRequestException)
            {
                Status = "Cannot get device info due to network error";
                ToggleBlocks.ActivityIndicator = false;
                return;
            }

            Name = device.Name;
            IsDeviceActive = QueryTimer.CompareTimes(device.LastActive);

            if (!IsDeviceActive)
            {
                EnableOrDisableAllButtons(false);
            }
            else
            {
                _isEnableShutdownButton = !device.IsShutdownPending;
                _isEnableRestartButton = !device.IsRestartPending;
                _isEnableSleepButton = !device.IsSleepPending;
                _isEnableMuteButton = !device.IsMutePending;

                Device.BeginInvokeOnMainThread(() =>
                {
                    ShutdownButtonCommand.ChangeCanExecute();
                    RestartButtonCommand.ChangeCanExecute();
                    SleepButtonCommand.ChangeCanExecute();
                    MuteButtonCommand.ChangeCanExecute();
                });
            }

            Status = string.Empty;
            ToggleBlocks.ActivityIndicator = false;
        }

        private void EnableOrDisableAllButtons(bool enableOrDisable)
        {
            _isEnableShutdownButton = enableOrDisable;
            _isEnableRestartButton = enableOrDisable;
            _isEnableSleepButton = enableOrDisable;
            _isEnableMuteButton = enableOrDisable;

            Device.BeginInvokeOnMainThread(() =>
            {
                ShutdownButtonCommand.ChangeCanExecute();
                RestartButtonCommand.ChangeCanExecute();
                SleepButtonCommand.ChangeCanExecute();
                MuteButtonCommand.ChangeCanExecute();
            });
        }

        private async void DoAction(Actions action)
        {
            EnableOrDisableAllButtons(false);
            ToggleBlocks.ActivityIndicator = true;

            try
            {
                await RequestHandler.DoRequest(Client.SetAction(_deviceId, action));
            }
            catch (Exception)
            {
                Status = "Cannot send request due to network error";
                return;
            }
            finally
            {
                EnableOrDisableAllButtons(true);
                ToggleBlocks.ActivityIndicator = false;
            }

            if (action == Actions.Sleep || action == Actions.Shutdown)
                new Thread(WaitTillSuspendAndMakeNotification).Start();

            DependencyService.Get<IMessage>().ShortAlert("Action request was successfully sent...");
            Status = String.Empty;
        }

        private async void WaitTillSuspendAndMakeNotification()
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            var options = new NotificationOptions
            {
                Title = "Action failed",
                Description = "Device was not suspended successfully"
            };

            for (int counter = 0; counter < (15 * 60) / QueryTimer.TimeBetweenQuery; counter++)
            {
                Thread.Sleep(QueryTimer.TimeBetweenQuery * 1000);
                try
                {
                    var device = await Client.GetDevice(_deviceId);
                    var isDeviceActive = QueryTimer.CompareTimes(device.LastActive);

                    if (!isDeviceActive)
                    {
                        Name = device.Name;
                        IsDeviceActive = false;
                        EnableOrDisableAllButtons(false);

                        options.Title = "Action successful";
                        options.Description = "Device was suspended successfully";
                        break;
                    }
                }
                catch (Exception)
                {
                    options.Title = "Network error";
                    options.Description = "Please check your network connection";
                    break;
                }
            }

            await notificator.Notify(options);
        }

        public Command ShutdownButtonCommand
        {
            get
            {
                return _shutdownButtonCommand ?? (_shutdownButtonCommand = new Command(p => DoAction(Actions.Shutdown), p => _isEnableShutdownButton));
            }
        }

        public Command RestartButtonCommand
        {
            get
            {
                return _restartButtonCommand ?? (_restartButtonCommand = new Command(p => DoAction(Actions.Restart), p => _isEnableRestartButton));
            }
        }

        public Command SleepButtonCommand
        {
            get
            {
                return _sleepButtonCommand ?? (_sleepButtonCommand = new Command(p => DoAction(Actions.Sleep), p => _isEnableSleepButton));
            }
        }

        public Command MuteButtonCommand
        {
            get
            {
                return _muteButtonCommand ?? (_muteButtonCommand = new Command(p => DoAction(Actions.Mute), p => _isEnableMuteButton));
            }
        }
    }
}
