using Foglalas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.Services
{
    public interface ILoginService
    {
        Task<User> Login(string username, string password);
    }
}
