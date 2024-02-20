using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public Restaurant Restaurant { get; set; }
        public User User { get; set; }
        public int Size { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishedTime { get; set; }
        public bool Outdoor { get; set; }
        public bool SeperateRoom { get; set; }
    }
}
