using CommunityToolkit.Maui.Views;
using Foglalas.ViewModels;
using Foglalas.Models;

namespace Foglalas.Views;

public partial class ListPage : ContentPage
{
	public ListPage(ListPageViewModel listPageViewModel)
	{
		InitializeComponent();
		BindingContext = listPageViewModel;
	}
}