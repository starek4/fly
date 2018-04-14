using System;
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
        private bool _isBusy;

        private Models.Device _device;
        private string _status;
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public DeviceActionViewModel(string deviceId)
        {
            GetDeviceAndEditButtonsState(deviceId);
        }

        private async void GetDeviceAndEditButtonsState(string deviceId)
        {
            _device = await RequestHandler.DoRequest(Client.GetDevice(deviceId));
            Name = _device.Name;
            bool isActive = DateTime.Now.Subtract(_device.LastActive).TotalSeconds < 60;
            Status = isActive ? "Active" : "Inactive";

            if (!isActive)
            {
                _isEnableShutdownButton = false;
                _isEnableRestartButton = false;
                _isEnableSleepButton = false;
                _isEnableMuteButton = false;
            }
            else
            {
                _isEnableShutdownButton = !_device.IsShutdownPending;
                _isEnableRestartButton = !_device.IsRestartPending;
                _isEnableSleepButton = !_device.IsSleepPending;
                _isEnableMuteButton = !_device.IsMutePending;
            }

            ShutdownButtonCommand.ChangeCanExecute();
            RestartButtonCommand.ChangeCanExecute();
            SleepButtonCommand.ChangeCanExecute();
            MuteButtonCommand.ChangeCanExecute();
        }

        private void SetBusy(bool isBusy)
        {
            _isBusy = isBusy;
        }

        private async void Shutdown()
        {
            await RequestHandler.DoRequest(Client.SetAction(_device.DeviceId, Actions.Shutdown));
        }

        private async void Restart()
        {
            await RequestHandler.DoRequest(Client.SetAction(_device.DeviceId, Actions.Restart));
        }

        private async void Sleep()
        {
            await RequestHandler.DoRequest(Client.SetAction(_device.DeviceId, Actions.Sleep));
        }

        private async void Mute()
        {
            await RequestHandler.DoRequest(Client.SetAction(_device.DeviceId, Actions.Mute));
        }

        public Command ShutdownButtonCommand
        {
            get
            {
                return _shutdownButtonCommand ?? (_shutdownButtonCommand = new Command(p => Shutdown(), p => _isEnableShutdownButton));
            }
        }

        public Command RestartButtonCommand
        {
            get
            {
                return _restartButtonCommand ?? (_restartButtonCommand = new Command(p => Restart(), p => _isEnableRestartButton));
            }
        }

        public Command SleepButtonCommand
        {
            get
            {
                return _sleepButtonCommand ?? (_sleepButtonCommand = new Command(p => Sleep(), p => _isEnableSleepButton));
            }
        }

        public Command MuteButtonCommand
        {
            get
            {
                return _muteButtonCommand ?? (_muteButtonCommand = new Command(p => Mute(), p => _isEnableMuteButton));
            }
        }
    }
}
