using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foglalas.Models;
using CommunityToolkit.Mvvm.Input;

namespace Foglalas.ViewModels
{
    public partial class AcceptPopupViewModel : ObservableObject
    {
        public City PickedCity { get; set; }
        public Restaurant PickedRestaurant { get; set; }
        public int GivenSize { get; set; }
        public string GivenName { get; set; }
        public DateTime PickedDate { get; set; }
    }
}
