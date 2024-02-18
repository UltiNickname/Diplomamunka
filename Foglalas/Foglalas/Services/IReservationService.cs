using Foglalas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.Services
{
    public interface IReservationService
    {
        Task<string> Reserve(Restaurant restaurant, User user, int size, DateTime date, bool outdoor, bool seperate);
    }
}
