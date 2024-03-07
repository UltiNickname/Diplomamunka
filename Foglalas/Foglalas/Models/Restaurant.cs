using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
        public TimeOnly Opening { get; set; }
        public TimeOnly Closing { get; set; }
        public TimeOnly KitchenClosing { get; set; }
        public bool Outdoor { get; set; }
        public bool SeperateRoom { get; set; }
        public bool FixedTables { get; set; }
        public bool Menu { get; set; }
        public bool AnimalFriendly { get; set; }
        public bool SzepKartyaAvailable { get; set; }
    }
}
