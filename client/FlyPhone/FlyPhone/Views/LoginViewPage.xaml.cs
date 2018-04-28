using FlyPhone.ViewModels;

namespace FlyPhone.Views
{
	public partial class LoginViewPage
	{
		public LoginViewPage()
		{
			InitializeComponent();
		    BindingContext = new LoginViewModel(Navigation);
		}
	}
}
