﻿using Foglalas.Models;
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

        public async Task<List<Reservation>> Reservations(int userId)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var reservationList = new List<Reservation>();
                    var client = new HttpClient();
                    string url = "http://192.168.0.80:8099/api/reservation/GetUserAll/" + userId.ToString();
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var client = new HttpClient();
                    string url = "http://192.168.0.80:8099/api/reservation/Delete?reservationId=" + id;
                    client.BaseAddress = new Uri(url);
                    HttpResponseMessage response = await client.DeleteAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
