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
    public partial class SignUpPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _emailAddress;
        [ObservableProperty]
        private string _userName;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private string _passwordAgain;

        readonly ISignUpService signUpService = new SignUpService();

        [RelayCommand]
        public async Task ToLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        [RelayCommand]
        public async Task SignUp()
        {
            if (!string.IsNullOrWhiteSpace(UserName) && 
                !string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(EmailAddress) &&
                !string.IsNullOrWhiteSpace(PasswordAgain))
            {
                if (!Password.Equals(PasswordAgain))
                {
                    await Shell.Current.DisplayAlert("Alert!", "The password are not matching!", "OK");
                    return;
                }

                User newUser = new User(){
                    Email = EmailAddress, 
                    Password = Password,
                    Username = UserName
                };
                string userInfo = await signUpService.SignUp(newUser);

                if(userInfo == "User created!")
                {
                    await Shell.Current.DisplayAlert("Success!", "User created!", "OK");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Alert!", "User with given email address already exists.", "OK");
                }
            }
            else
                await Shell.Current.DisplayAlert("Alert!", "Fill all the required fields!", "OK");
        }
    }
}
