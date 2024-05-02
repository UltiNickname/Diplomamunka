using Foglalas.ViewModels;
using Foglalas.Views;

namespace Foglalas;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();
		this.BindingContext = new AppShellViewModel();
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
		Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
	}
}
