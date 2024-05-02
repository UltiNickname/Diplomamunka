﻿using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [ICommand]
        async void SignOut()
        {
            if(Preferences.ContainsKey(nameof(App.User)))
            {
                Preferences.Remove(nameof(App.User));
            }
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
