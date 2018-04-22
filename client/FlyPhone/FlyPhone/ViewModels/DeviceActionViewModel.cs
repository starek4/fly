using System;
using System.Threading;
using FlyClientApi.Enums;
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

        private Models.Device _device;
        private string _deviceStatus;
        private string _status;
        private string _name;
        private readonly string _deviceId;

        public Toggle ToggleBlocks { get; set; } = new Toggle();
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string DeviceStatus
        {
            get => _deviceStatus;
            set
            {
                _deviceStatus = value;
                OnPropertyChanged(nameof(DeviceStatus));
            }
        }

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

            _device = await RequestHandler.DoRequest(Client.GetDevice(_deviceId));
            Name = _device.Name;
            bool isActive = DateTime.Now.Subtract(_device.LastActive).TotalSeconds < 60;
            DeviceStatus = isActive ? "Active" : "Inactive";

            if (!isActive)
            {
                EnableOrDisableAllButtons(false);
            }
            else
            {
                _isEnableShutdownButton = !_device.IsShutdownPending;
                _isEnableRestartButton = !_device.IsRestartPending;
                _isEnableSleepButton = !_device.IsSleepPending;
                _isEnableMuteButton = !_device.IsMutePending;

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

            await RequestHandler.DoRequest(Client.SetAction(_deviceId, action));

            EnableOrDisableAllButtons(true);
            ToggleBlocks.ActivityIndicator = false;

            DependencyService.Get<IMessage>().ShortAlert("Action request was successfully sent...");
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
