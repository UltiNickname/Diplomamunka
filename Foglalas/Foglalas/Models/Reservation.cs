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
        public int Restaurant { get; set; }
        public int User { get; set; }
        public int Size { get; set; }
        public DateOnly Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishedTime { get; set; }
        public bool Outdoor { get; set; }
        public bool SeperateRoom { get; set; }
    }
}
