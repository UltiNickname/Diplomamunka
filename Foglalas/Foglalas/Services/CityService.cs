using Foglalas.Models;
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
        public Task<List<City>> Cities()
        {
            List<City> cities = new List<City>()
            {
                new City() { Id = 1, Name="Budapest"},
                new City() { Id = 2, Name="Szombathely"},
                new City() { Id = 3, Name="Veszprém"}
            };
            return Task.FromResult(cities);
        }
    }
}
