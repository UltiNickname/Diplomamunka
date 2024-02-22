using Foglalas.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.Services
{
    internal class ReservationService : IReservationService
    {
        public async Task<string> Reserve(Reservation reservation)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var reservationInfo = new List<Reservation>();
                    var client = new HttpClient();
                    string url = "http://localhost:8099/api/Reservation/AddNewReservation";
                    client.BaseAddress = new Uri(url);
                    var body = JsonConvert.SerializeObject(reservation);
                    HttpResponseMessage response = await client.PostAsJsonAsync(url, body);
                    if (response.IsSuccessStatusCode)
                    {
                        return "Reservation successfull!";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
