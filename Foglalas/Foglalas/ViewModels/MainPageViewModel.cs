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
using Foglalas.Views;

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
                        ClosedMonday = _selectedRestaurant.ClosedOnMonday;
                        ClosedSunday = _selectedRestaurant.ClosedOnSunday;
                        IsRestaurantPicked = true;
                        IsFreeTable = !_selectedRestaurant.FixedTables;
                        GivenSize = null;
                        GivenTable = 0;
                        if (HasFixedTables)
                            LoadTables(_selectedRestaurant.RestaurantId);
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
        private int _givenTable;

        [ObservableProperty]
        private string _givenName;

        private DateTime _pickedDate;
        public DateTime PickedDate
        {
            get => _pickedDate;
            set
            {
                if (_pickedDate != value)
                {
                    _pickedDate = value;
                    GetCapacity(_selectedRestaurant.RestaurantId, DateOnly.FromDateTime(PickedDate), PickedStartTime, PickedEndTime);
                }
            }
        }

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
                    GetCapacity(_selectedRestaurant.RestaurantId, DateOnly.FromDateTime(PickedDate), PickedStartTime, PickedEndTime);
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
                    GetCapacity(_selectedRestaurant.RestaurantId, DateOnly.FromDateTime(PickedDate), PickedStartTime, PickedEndTime);
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
        private bool _isTimeOkayR;

        [ObservableProperty]
        private int _maxCapacity;

        [ObservableProperty]
        private int _currentCapacity;

        [ObservableProperty]
        private bool _isFreeTable;

        [ObservableProperty]
        private int _tableNumber;

        [ObservableProperty]
        private bool _closedMonday;

        [ObservableProperty]
        private bool _closedSunday;

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
        private bool _isSeperateRoomAvailable;

        [ObservableProperty]
        private DateTime _minDate = DateTime.Today.AddDays(1);

        [ObservableProperty]
        private DateTime _maxDate = DateTime.Today.AddDays(122);
        public ObservableRangeCollection<City> Cities { get; set; } = new();
        public ObservableRangeCollection<Restaurant> Restaurants { get; set; } = new();
        public ObservableRangeCollection<int> Tables { get; set; } = new();
        public MainPageViewModel(ICityService cityService) 
        {
            LoadCities();
            HasFixedTables = false;
            IsFreeTable = true;
            IsTimeOkay = false;
            IsTimeOkayR = false;
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
        private async void LoadTables(int id)
        {
            var tables = await cityService.Tables(id);
            if (tables == null)
                return;
            if (Tables.Count > 0)
                Tables.Clear();
            Tables.AddRange(tables);
        }

        private async void GetCapacity(int id, DateOnly date, TimeSpan start, TimeSpan finish)
        {
            MaxCapacity = await cityService.MaxCapacity(id);
            CurrentCapacity = await cityService.CurrentCapacity(id, date, start, finish);
        }
        public void CheckTime()
        {
            IsNearClosing = false;
            if (OpeningTime <= TimeOnly.FromTimeSpan(PickedStartTime) && 
                TimeOnly.FromTimeSpan(PickedEndTime) <= ClosingTime && 
                TimeOnly.FromTimeSpan(PickedStartTime) < TimeOnly.FromTimeSpan(PickedEndTime) && 
                TimeOnly.FromTimeSpan(PickedStartTime) < TimeOnly.FromTimeSpan(ClosingTime.ToTimeSpan().Subtract(new TimeSpan(0, 30, 0))))
            {
                if(HasMenu &&  TimeOnly.FromTimeSpan(Menustart) < TimeOnly.FromTimeSpan(PickedEndTime) && TimeOnly.FromTimeSpan(PickedStartTime) < TimeOnly.FromTimeSpan(Menuend))
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
            IsTimeOkayR = !IsTimeOkay;
        }

        public async void SeperateRoomAvailable(int id, DateOnly date)
        {
            IsSeperateRoomAvailable = await cityService.SeperateRoomAvailability(id, date);
        }

        public async Task<int> TableAvailable(int id, int size, DateOnly date, TimeSpan start, TimeSpan finish) => TableNumber = await cityService.AvailableTable(id, size, date, start, finish);

        [RelayCommand]
        public async Task MakeReservation()
        {
            if (GivenSize == null)
                GivenSize = "0";
            if (int.Parse(GivenSize) > CurrentCapacity || GivenTable > CurrentCapacity)
            {
                await Shell.Current.DisplayAlert("Sajnáljuk!", "Az étterem nem tud ekkora társaságot az Ön által megadott időpontban.", "OK");
                return;
            }
            if ((ClosedMonday && PickedDate.DayOfWeek.ToString() == "Monday") || (ClosedSunday && PickedDate.DayOfWeek.ToString() == "Sunday"))
            {
                await Shell.Current.DisplayAlert("Sajnáljuk!", "Az étterem Nincs nyitva az adott napon.", "OK");
                return;
            }
            if (SelectedCity != null && SelectedRestaurant != null && GivenName != null && (GivenSize != null || GivenTable != 0))
            {
                    SeperateRoomAvailable(SelectedRestaurant.RestaurantId, DateOnly.FromDateTime(PickedDate));
                    await TableAvailable(SelectedRestaurant.RestaurantId, GivenTable, DateOnly.FromDateTime(PickedDate), PickedStartTime, PickedEndTime);
                if (SelectedRestaurant.SeperateRoom && !IsSeperateRoomAvailable && SeperateRoom)
                {
                    await Shell.Current.DisplayAlert("Sajnáljuk!", "Az Ön által választott napra már lefoglalták a különtermet.", "OK");
                }
                else if (SelectedRestaurant.FixedTables && TableNumber == 0)
                {
                    await Shell.Current.DisplayAlert("Sajnáljuk!", "Az Ön által választott asztal méret már nem elérhető az ön által kívánt időpontban.", "OK");
                }
                else
                {
                    if (PickedDate == default(DateTime))
                    {
                        PickedDate = MinDate;
                    }
                    bool answer;
                    if (HasFixedTables)
                    {
                        answer = await Shell.Current.DisplayAlert
                                ("Foglalás adatai:", "Biztosan szeretne foglalni?\n\n"
                                + SelectedRestaurant.Name + "\n"
                                + GivenName + "\n"
                                + GivenTable + " fő \n"
                                + PickedDate.Add(PickedStartTime).ToString() + "-" + PickedDate.Add(PickedEndTime).ToString(), "Yes", "No");
                    }
                    else
                    {
                        answer = await Shell.Current.DisplayAlert
                                ("Foglalás adatai:", "Biztosan szeretne foglalni?\n\n"
                                + SelectedRestaurant.Name + "\n"
                                + GivenName + "\n"
                                + GivenSize + " fő \n"
                                + PickedDate.Add(PickedStartTime).ToString() + "-" + PickedDate.Add(PickedEndTime).ToString(), "Yes", "No");
                    }
                    if (answer)
                    {
                        ReservationService reservationService = new ReservationService();
                        if (_selectedRestaurant.FixedTables)
                        {
                            Reservation newReservation = new Reservation()
                            {
                                Restaurant = SelectedRestaurant,
                                User = App.User,
                                Name = GivenName,
                                Size = GivenTable,
                                Date = DateOnly.FromDateTime(PickedDate),
                                StartTime = PickedStartTime.Subtract(new TimeSpan(0, 30, 0)),
                                FinishedTime = PickedEndTime.Add(new TimeSpan(0, 30, 0)),
                                Outdoor = Terrace && IsTerraceEnable,
                                SeperateRoom = SeperateRoom && IsSeperateRoomEnable
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
                        else
                        {
                            Reservation newReservation = new Reservation()
                            {
                                Restaurant = SelectedRestaurant,
                                User = App.User,
                                Name = GivenName,
                                Size = int.Parse(GivenSize),
                                Date = DateOnly.FromDateTime(PickedDate),
                                StartTime = PickedStartTime.Subtract(new TimeSpan(0, 30, 0)),
                                FinishedTime = PickedEndTime.Add(new TimeSpan(0, 30, 0)),
                                Outdoor = Terrace && IsTerraceEnable,
                                SeperateRoom = SeperateRoom && IsSeperateRoomEnable
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
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Figyelem!", "Töltse ki az össes mezőt", "OK");
            }
        }
    }
};
