using FlyPhone.ViewModels;

namespace FlyPhone
{
	public partial class LoginPage
	{
	    public LoginPage()
	    {
	        InitializeComponent();
	        BindingContext = new LoginPageViewModel(Navigation);
        }
    }
}
