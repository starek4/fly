using System.ComponentModel;
using Xamarin.Forms;

namespace FlyPhone.ViewModels.Base
{
    public class BaseViewModel : ContentPage, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
