using CommunityToolkit.Maui.Views;
using Foglalas.ViewModels;

namespace Foglalas.Views;

public partial class DeniedPopup : Popup
{
    public DeniedPopup(DeniedPopupViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void OKButton_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}