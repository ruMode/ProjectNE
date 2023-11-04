using ProjectNE.ViewModels;

namespace ProjectNE.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		LoginPageVM vm = new LoginPageVM();
		BindingContext =vm ;
	}

}