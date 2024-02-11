using CommunityToolkit.Maui.Views;
using Foglalas.ViewModels;
using Foglalas.Views;
using Foglalas.Models;

namespace Foglalas;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
        BindingContext = mainPageViewModel;
	}
}

