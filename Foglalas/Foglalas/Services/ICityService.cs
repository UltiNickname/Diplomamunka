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
    }
}
