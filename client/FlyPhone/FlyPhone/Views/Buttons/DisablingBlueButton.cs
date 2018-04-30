using Xamarin.Forms;

namespace FlyPhone.Views.Buttons
{
    public class DisablingBlueButton : Button
    {
        private readonly Style _normalStyle;
        private readonly Style _disableStyle;

        public DisablingBlueButton()
        {
            _normalStyle = Application.Current.Resources["StickyBlueButton"] as Style;
            _disableStyle = Application.Current.Resources["StickyBlueButtonDisabled"] as Style;
            Style = _normalStyle;
            PropertyChanged += ExtendedButton_PropertyChanged;
        }

        private void ExtendedButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsEnabled))
            {
                Style = IsEnabled ? _normalStyle : _disableStyle;
            }
        }
    }
}
