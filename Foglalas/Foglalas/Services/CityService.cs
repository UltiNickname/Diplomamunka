using Foglalas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.Services
{
    public partial class CityService : ICityService
    {
        public async Task<List<Restaurant>> Restaurants()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var restaurantList = new List<Restaurant>();
                    var client = new HttpClient();
                    string url = "http://192.168.0.80:8099/api/restaurant/GetAll";
                    client.BaseAddress = new Uri(url);
                    HttpResponseMessage response = await client.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        restaurantList = JsonConvert.DeserializeObject<List<Restaurant>>(json);
                        return restaurantList;
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
        public async Task<List<City>> Cities()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var cityList = new List<City>();
                    var client = new HttpClient();
                    string url = "http://192.168.0.80:8099/api/city/GetAll";
                    client.BaseAddress = new Uri(url);
                    HttpResponseMessage response = await client.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        cityList = JsonConvert.DeserializeObject<List<City>>(json);
                        return cityList;
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
