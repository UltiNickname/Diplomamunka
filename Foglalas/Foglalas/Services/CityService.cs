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
        public Task<List<Restaurant>> Restaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>()
            {
                new Restaurant() {Id = 1, Name="Trófea", CityId=1},
                new Restaurant() {Id = 2, Name="Fórum", CityId=2},
                new Restaurant() {Id = 3, Name="West Garden", CityId=2},
                new Restaurant() {Id = 4, Name="Gekko", CityId=2},
                new Restaurant() {Id = 5, Name="Marica", CityId=3},
                new Restaurant() {Id = 6, Name="Fejesvölgy Étterem", CityId=3},
                new Restaurant() {Id = 7, Name="Sport vendéglő", CityId=3}
            };
            return Task.FromResult(restaurants);
        }
        public async Task<List<City>> Cities()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var cityList = new List<City>();
                    var client = new HttpClient();
                    string url = "http://localhost:8099/api/city/GetAll";
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
