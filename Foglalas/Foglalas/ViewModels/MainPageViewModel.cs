﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using Foglalas.Models;
using Foglalas.Services;
using Foglalas.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.ViewModels
{
    public partial class MainPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public ICityService cityService = new CityService();

        [ObservableProperty]
        Restaurant _selectedRestaurant;

        [ObservableProperty]
        string _givenSize;

        [ObservableProperty]
        string _givenName;

        [ObservableProperty]
        DateTime _pickedDate;

        [ObservableProperty]
        TimeSpan _pickedTime;

        [ObservableProperty]
        bool _isRestaurantEnabled;

        [ObservableProperty]
        DateTime _minDate = DateTime.Today.AddDays(1);

        [ObservableProperty]
        DateTime _maxDate = DateTime.Today.AddDays(10);

        private City _selectedCity;
        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    IsRestaurantEnabled = true;
                    LoadRestaurants(SelectedCity.Id);
                }
            }
        }
        public ObservableRangeCollection<City> Cities { get; set; } = new();
        public ObservableRangeCollection<Restaurant> Restaurants { get; set; } = new();
        public MainPageViewModel(ICityService cityService) 
        {
            LoadCities();
            IsRestaurantEnabled = false;
            this.cityService = cityService;
        }

        private async void LoadCities()
        {
            var cities = await cityService.Cities();
            if(cities == null)
                return;
            if(Cities.Count > 0)
                Cities.Clear();
            Cities.AddRange(cities);
        }
        private async void LoadRestaurants(int id)
        {
            var restaurants = await cityService.Restaurants();
            if (restaurants == null)
                return;
            if (Restaurants.Count > 0)
                Restaurants.Clear();
            Restaurants.AddRange(restaurants.Where(i => i.CityId == id).ToList());
        }

        [RelayCommand]
        public void DisplaySelected()
        {
            if (SelectedCity != null && SelectedRestaurant != null)
            {
                PickedDate=PickedDate.Add(PickedTime);
                AcceptPopupViewModel acceptPopupViewModel = new AcceptPopupViewModel()
                {
                    PickedCity = this.SelectedCity.Name,
                    PickedRestaurant = this.SelectedRestaurant.Name,
                    GivenSize = this.GivenSize,
                    GivenName = this.GivenName,
                    PickedDate = this.PickedDate.ToString(),
                };
                var popup = new AcceptPopup(acceptPopupViewModel);

                Shell.Current.ShowPopupAsync(popup);
            }
            else
            {
                DeniedPopupViewModel deniedPopupViewModel = new DeniedPopupViewModel
                {
                    Text = "Töltse ki az össes mezőt!"
                };
                var popup = new DeniedPopup(deniedPopupViewModel);

                Shell.Current.ShowPopupAsync(popup);
            }
        }
    }
};
