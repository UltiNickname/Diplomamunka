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
                    string url = "http://192.168.0.80:8099/api/reservation/AddNewReservation";
                    client.BaseAddress = new Uri(url);
                    var body = JsonConvert.SerializeObject(reservation);
                    HttpResponseMessage response = await client.PostAsJsonAsync(url, reservation);
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

        public async Task<List<Reservation>> Reservations(User user)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var reservationList = new List<Reservation>();
                    var client = new HttpClient();
                    string url = "http://192.168.0.80:8099/api/reservation/GetUserAll";
                    client.BaseAddress = new Uri(url);
                    HttpResponseMessage response = await client.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        reservationList = JsonConvert.DeserializeObject<List<Reservation>>(json);
                        return reservationList;
                    }
                    else
                    {
                        return null;
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
