using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foglalas.Models;

namespace Foglalas.ViewModels
{
    public partial class AcceptPopupViewModel : ObservableObject
    {
        public string PickedCity { get; set; }
        public string PickedRestaurant { get; set; }
        public string GivenSize { get; set; }
        public string GivenName { get; set; }
        public string PickedDate { get; set; }
    }
}
