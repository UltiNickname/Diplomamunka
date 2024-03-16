using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Foglalas.Services;
using Foglalas.ViewModels;
using Foglalas.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace Foglalas;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<ICityService, CityService>();
		builder.Services.AddSingleton<IReservationService, ReservationService>();
		builder.Services.AddSingleton<ILoginService, LoginService>();
		builder.Services.AddSingleton<ISignUpService, SignUpService>();

        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

		builder.Services.AddSingleton<ListPageViewModel>();
		builder.Services.AddSingleton<ListPage>();

		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddSingleton<LoginPageViewModel>();

		builder.Services.AddSingleton<SignUpPage>();
		builder.Services.AddSingleton<SignUpPageViewModel>();

        builder.Services.AddSingleton<AdminPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
