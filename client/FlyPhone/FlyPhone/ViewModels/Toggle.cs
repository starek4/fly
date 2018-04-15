using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlyPhone.ViewModels
{
    public class Toggle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _activityIndicator = true;
        public bool ActivityIndicator
        {
            get => _activityIndicator;
            set
            {
                _activityIndicator = value;
                OnPropertyChanged(nameof(ActivityIndicator));
                OnPropertyChanged(nameof(OtherBlock));
            }
        }
        public bool OtherBlock
        {
            get => !_activityIndicator;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
