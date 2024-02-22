using CommunityToolkit.Mvvm.ComponentModel;
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
        private string _givenSize;

        [ObservableProperty]
        private string _givenName;

        [ObservableProperty]
        private string _position;

        [ObservableProperty]
        private DateTime _pickedDate;

        [ObservableProperty]
        private TimeSpan _pickedTime;

        [ObservableProperty]
        private bool _isRestaurantEnabled;

        [ObservableProperty]
        private bool _isTerraceEnable;

        [ObservableProperty]
        private bool _isSeperateRoomEnable;

        [ObservableProperty]
        private DateTime _minDate = DateTime.Today.AddDays(1);

        [ObservableProperty]
        private DateTime _maxDate = DateTime.Today.AddDays(10);

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
                    LoadRestaurants(SelectedCity.CityId);
                }
            }
        }

        private Restaurant _selectedRestaurant;
        public Restaurant SelectedRestaurant
        {
            get { return _selectedRestaurant; }
            set
            {
                if (_selectedRestaurant != value)
                {
                    _selectedRestaurant = value;
                    if(_selectedRestaurant != null)
                    {
                        IsTerraceEnable = _selectedRestaurant.Outdoor;
                        IsSeperateRoomEnable = _selectedRestaurant.SeperateRoom;
                    }
                }
            }
        }
        public ObservableRangeCollection<City> Cities { get; set; } = new();
        public ObservableRangeCollection<Restaurant> Restaurants { get; set; } = new();
        public MainPageViewModel(ICityService cityService) 
        {
            LoadCities();
            IsRestaurantEnabled = false;
            IsTerraceEnable = false;
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
            Restaurants.AddRange(restaurants.Where(i => i.City.CityId == id).ToList());
        }

        [RelayCommand]
        public async Task MakeReservation()
        {
            if (SelectedCity != null && SelectedRestaurant != null && GivenName != null && GivenSize != null)
            {
                bool answer = await Shell.Current.DisplayAlert
                    ("Question?", "Would you like to make your reservation?\n\n"
                    + SelectedRestaurant.Name + "\n"
                    + GivenName + "\n"
                    + GivenSize+ " fő \n"
                    + PickedDate.Add(PickedTime).ToString(), "Yes", "No");
                if (answer)
                {
                    ReservationService reservationService = new ReservationService();

                    bool od, sr;
                    if (Position == null)
                    {
                        od = false; sr = false;
                    }
                    else if (Position == "Terrace")
                    {
                        od = true; sr = false;
                    }
                    else
                    {
                        od = false; sr = true;
                    }

                    Reservation newReservation = new Reservation()
                    {
                        Restaurant = SelectedRestaurant,
                        User = App.User,
                        Size = int.Parse(GivenSize),
                        Date = DateOnly.FromDateTime(PickedDate),
                        StartTime = PickedTime,
                        FinishedTime = PickedTime.Add(new TimeSpan(0, 0, int.Parse(GivenSize)*20, 0)),
                        Outdoor = od,
                        SeperateRoom = sr
                    };
                    string reservationInfo = await reservationService.Reserve(newReservation);
                    if (reservationInfo == "Reservation successfull!")
                    {
                        await Shell.Current.DisplayAlert("Success!", "Rerevation has been made!", "OK");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Alert!", "Error", "OK");
                    }
                }                
            }
            else
            {
                DeniedPopupViewModel deniedPopupViewModel = new DeniedPopupViewModel
                {
                    Text = "Töltse ki az össes mezőt!"
                };
                var popup = new DeniedPopup(deniedPopupViewModel);

                await Shell.Current.ShowPopupAsync(popup);
            }
        }
    }
};
