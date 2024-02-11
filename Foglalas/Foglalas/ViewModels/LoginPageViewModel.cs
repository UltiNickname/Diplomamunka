using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foglalas.Models;
using Foglalas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Foglalas.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;
        [ObservableProperty]
        private string _password;

        readonly ILoginService loginService = new LoginService();

        [RelayCommand]
        public async void Login()
        {
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
            {
                User userInfo = await loginService.Login(UserName, Password);

                if(Preferences.ContainsKey(nameof(App.User)))
                {
                    Preferences.Remove(nameof(App.User));
                }

                string userDetails=JsonSerializer.Serialize(userInfo);
                Preferences.Set(nameof(App.User), userDetails);
                App.User = userInfo;

                await Shell.Current.GoToAsync(nameof(MainPage));
            }
        }

        [RelayCommand]
        public async void ToSignUp()
        {
            await Shell.Current.GoToAsync(nameof(SignUpPage));
        }
    }
}
