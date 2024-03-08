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
                        HasMenu = _selectedRestaurant.Menu;
                        HasFixedTables = _selectedRestaurant.FixedTables;
                        SzepCard = _selectedRestaurant.SzepKartyaAvailable;
                        GetCapacity(_selectedRestaurant.RestaurantId, DateOnly.FromDateTime(PickedDate), PickedStartTime, PickedEndTime);
                        OpeningTime = _selectedRestaurant.Opening;
                        ClosingTime = _selectedRestaurant.Closing;
                        KitchenClosing = _selectedRestaurant.KitchenClosing;
                        IsRestaurantPicked = true;
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

        private TimeSpan _pickedStartTime;
        public TimeSpan PickedStartTime
        {
            get => _pickedStartTime;
            set
            {
                if (_pickedStartTime != value)
                {
                    _pickedStartTime = value;
                    CheckTime();
                }
            }
        }

        private TimeSpan _pickedEndTime;
        public TimeSpan PickedEndTime
        {
            get => _pickedEndTime;
            set
            {
                if (_pickedEndTime != value)
                {
                    _pickedEndTime = value;
                    CheckTime();
                }
            }
        }

        //Hidden attributes

        [ObservableProperty]
        private bool _isRestaurantEnabled;

        [ObservableProperty]
        private bool _isRestaurantPicked;

        [ObservableProperty]
        private bool _isAnimalFriendly;

        [ObservableProperty]
        private bool _hasMenu;

        [ObservableProperty]
        private bool _hasFixedTables;

        [ObservableProperty]
        private bool _szepCard;

        [ObservableProperty]
        private bool _isTerraceEnable;

        [ObservableProperty]
        private bool _isSeperateRoomEnable;

        [ObservableProperty]
        private bool _isTimeOkay;

        [ObservableProperty]
        private int _maxCapacity;

        [ObservableProperty]
        private int _currentCapacity;

        [ObservableProperty]
        private TimeOnly _openingTime;

        [ObservableProperty]
        private TimeOnly _closingTime;

        [ObservableProperty]
        private TimeOnly _kitchenClosing;

        [ObservableProperty]
        TimeSpan _menustart = new TimeSpan(11, 00, 0);

        [ObservableProperty]
        TimeSpan _menuend = new TimeSpan(14, 00, 0);

        [ObservableProperty]
        private bool _isNearClosing;

        [ObservableProperty]
        private DateTime _minDate = DateTime.Today.AddDays(1);

        [ObservableProperty]
        private DateTime _maxDate = DateTime.Today.AddDays(122);
        public ObservableRangeCollection<City> Cities { get; set; } = new();
        public ObservableRangeCollection<Restaurant> Restaurants { get; set; } = new();
        public MainPageViewModel(ICityService cityService) 
        {
            LoadCities();
            IsTimeOkay = false;
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

        private async void GetCapacity(int id, DateOnly date, TimeSpan start, TimeSpan finish)
        {
            MaxCapacity = await cityService.MaxCapacity(id);
            CurrentCapacity = await cityService.CurrentCapacity(id, date, start, finish);
        }
        public async void CheckTime()
        {
            IsNearClosing = false;
            if (OpeningTime <= TimeOnly.FromTimeSpan(PickedStartTime) && TimeOnly.FromTimeSpan(PickedEndTime) <= ClosingTime && TimeOnly.FromTimeSpan(PickedStartTime) < TimeOnly.FromTimeSpan(PickedEndTime))
            {
                if(HasMenu &&  TimeOnly.FromTimeSpan(Menustart) <= TimeOnly.FromTimeSpan(PickedStartTime) && TimeOnly.FromTimeSpan(PickedEndTime) <= TimeOnly.FromTimeSpan(Menuend))
                    IsTimeOkay = false;
                else
                    IsTimeOkay = true;
                if (KitchenClosing < TimeOnly.FromTimeSpan(PickedEndTime))
                    IsNearClosing = true;
                else
                    IsNearClosing = false;
            }
            else
                IsTimeOkay = false;
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
                        Outdoor = Terrace&&IsTerraceEnable,
                        SeperateRoom = SeperateRoom&&IsSeperateRoomEnable
                    };
                    string reservationInfo = await reservationService.Reserve(newReservation);
                    if (reservationInfo == "Reservation successfull!")
                    {
                        await Shell.Current.DisplayAlert("Siker!", "A foglalás sikersen megtörtént!\nKöszönjük!", "OK");
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
