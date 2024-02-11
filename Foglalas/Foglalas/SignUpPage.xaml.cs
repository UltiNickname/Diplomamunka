using Foglalas.ViewModels;

namespace Foglalas;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpPageViewModel signUpPageViewModel)
	{
		InitializeComponent();
        this.BindingContext = signUpPageViewModel;
    }
}