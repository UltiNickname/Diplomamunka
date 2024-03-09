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
    public partial class ListPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Reservation _selectedReservation;

        readonly IReservationService reservationService = new ReservationService();
        public ObservableRangeCollection<Reservation> Reservations { get; set; } = new();
        public ListPageViewModel(IReservationService reservationService)
        {
            LoadReservations();
            this.reservationService = reservationService;
        }

        private async void LoadReservations()
        {
            var reservations = await reservationService.Reservations(App.User.UserId);
            if (reservations == null)
                return;
            if (Reservations.Count > 0)
                Reservations.Clear();
            Reservations.AddRange(reservations);
        }

        [RelayCommand]
        public async Task DeleteReservation()
        {
            SelectedReservation
        }
    }
}
