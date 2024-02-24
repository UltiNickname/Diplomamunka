using FoglalasAPI.Context;
using FoglalasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Drawing;

namespace FoglalasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private AppDbContext _appDbContext;

        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(ILogger<RestaurantController> logger, AppDbContext context)
        {
            _appDbContext = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return (from r in _appDbContext.Restaurants
                    select new Restaurant
                    {
                        RestaurantId = r.RestaurantId,
                        Name = r.Name,
                        City = r.City,
                        Outdoor = r.Outdoor,
                        SeperateRoom = r.SeperateRoom,
                        FixedTables = r.FixedTables
                    }).ToList();
        }

        [HttpGet]
        [Route("GetCapacity")]
        public int GetCapacity(int restaurantId)
        {
            return (from rt in _appDbContext.RestaurantTables
                    join t in _appDbContext.Tables on rt.Table.TableId equals t.TableId
                    where rt.Restaurant.RestaurantId == restaurantId
                    select rt.Count * t.Size).Sum();
        }

        [HttpPost]
        [Route("AddNewRestaurant")]
        public async Task<IActionResult> AddRestaurant(Restaurant restaurant)
        {
            Restaurant dbRestaurant = new Restaurant()
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                City = _appDbContext.Cities.Find(restaurant.City.CityId),
                Outdoor = restaurant.Outdoor,
                SeperateRoom = restaurant.SeperateRoom,
                FixedTables = restaurant.FixedTables
            };
            _appDbContext.Restaurants.Add(dbRestaurant);
            _appDbContext.SaveChanges();
            return Ok("Restaurant created!");
        }

        [HttpPost]
        [Route("AddTableToRestaurant")]
        public async Task<IActionResult> TableToRestaurant(RestaurantTables restaurantTables)
        {
            _appDbContext.Database.ExecuteSqlRaw($"INSERT INTO \"RestaurantTables\" (\"RestaurantId\", \"TableId\", \"Count\")VALUES ({restaurantTables.RestaurantId}, {restaurantTables.TableId}, {restaurantTables.Count})");
            _appDbContext.SaveChanges();
            return Ok("Table added created!");
        }
    }
}
