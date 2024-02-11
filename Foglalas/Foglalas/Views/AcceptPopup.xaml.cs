using CommunityToolkit.Maui.Views;
using Foglalas.ViewModels;

namespace Foglalas.Views;

public partial class AcceptPopup : Popup
{
	public AcceptPopup(AcceptPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private void OKButton_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}