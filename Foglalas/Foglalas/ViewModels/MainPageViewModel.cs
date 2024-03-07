using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using Foglalas.Models;
using Foglalas.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Foglalas.ViewModels
{
    public partial class MainPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public ICityService cityService = new CityService();
        
        //Form attributes

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
                    if (_selectedRestaurant != null)
                    {
                        IsTerraceEnable = _selectedRestaurant.Outdoor;
                        IsSeperateRoomEnable = _selectedRestaurant.SeperateRoom;
                        IsAnimalFriendly = _selectedRestaurant.AnimalFriendly;
                    }
                }
            }
        }

        [ObservableProperty]
        private bool _terrace;

        [ObservableProperty]
        private bool _seperateRoom;

        [ObservableProperty]
        private string _givenSize;

        [ObservableProperty]
        private string _givenName;

        [ObservableProperty]
        private DateTime _pickedDate;

        [ObservableProperty]
        private TimeSpan _pickedStartTime;

        [ObservableProperty]
        private TimeSpan _pickedEndTime;

        //Hidden attributes

        [ObservableProperty]
        private bool _isRestaurantEnabled;

        [ObservableProperty]
        private bool _isAnimalFriendly;

        [ObservableProperty]
        private bool _isTerraceEnable;

        [ObservableProperty]
        private bool _isSeperateRoomEnable;

        [ObservableProperty]
        private DateTime _minDate = DateTime.Today.AddDays(1);

        [ObservableProperty]
        private DateTime _maxDate = DateTime.Today.AddDays(122);
        public ObservableRangeCollection<City> Cities { get; set; } = new();
        public ObservableRangeCollection<Restaurant> Restaurants { get; set; } = new();
        public MainPageViewModel(ICityService cityService) 
        {
            LoadCities();
            IsRestaurantEnabled = false;
            IsTerraceEnable = false;
            IsRestaurantEnabled = false;
            IsAnimalFriendly = false;
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
                    ("Foglalás adatai:", "Biztosan szeretne foglalni?\n\n"
                    + SelectedRestaurant.Name + "\n"
                    + GivenName + "\n"
                    + GivenSize+ " fő \n"
                    + PickedDate.Add(PickedStartTime).ToString() +"-"+ PickedDate.Add(PickedEndTime).ToString(), "Yes", "No");
                if (answer)
                {
                    ReservationService reservationService = new ReservationService();

                    Reservation newReservation = new Reservation()
                    {
                        Restaurant = SelectedRestaurant,
                        User = App.User,
                        Name = GivenName,
                        Size = int.Parse(GivenSize),
                        Date = DateOnly.FromDateTime(PickedDate),
                        StartTime = PickedStartTime,
                        FinishedTime = PickedEndTime,
                        Outdoor = Terrace,
                        SeperateRoom = SeperateRoom
                    };
                    string reservationInfo = await reservationService.Reserve(newReservation);
                    if (reservationInfo == "Reservation successfull!")
                    {
                        await Shell.Current.DisplayAlert("Siker!", "A foglalás sikersen megtörtént!", "OK");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error!", "Valami hiba lépett fel", "OK");
                    }
                }                
            }
            else
            {
                await Shell.Current.DisplayAlert("Figyelem!", "Töltse ki az össes mezőt", "OK");
            }
        }
    }
};
