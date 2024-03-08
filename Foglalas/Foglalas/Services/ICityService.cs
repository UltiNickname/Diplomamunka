using Foglalas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.Services
{
    public interface ICityService
    {
        public Task<List<Restaurant>> Restaurants();
        public Task<List<City>> Cities();
        public Task<int> MaxCapacity(int id);
        public Task<int> CurrentCapacity(int id, DateOnly date, TimeSpan start, TimeSpan finish);
        public Task<bool> SeperateRoomAvailability(int id, DateOnly date);
    }
}
