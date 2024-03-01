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
    public partial class ListPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public IReservationService reservationService = new ReservationService();
        public ObservableRangeCollection<Reservation> Reservations { get; set; } = new();
        public ListPageViewModel(IReservationService reservationService)
        {
            LoadReservation(App.User.UserId);
            this.reservationService = reservationService;
        }
        private async void LoadReservation(int userId)
        {
            var restaurants = await reservationService.Reservations(userId);
            if (restaurants == null)
                return;
            if (Reservations.Count > 0)
                Reservations.Clear();
        }
    }
}
