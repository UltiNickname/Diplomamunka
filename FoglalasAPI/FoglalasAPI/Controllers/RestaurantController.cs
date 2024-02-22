﻿using FoglalasAPI.Context;
using FoglalasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                    join t in _appDbContext.Tables on rt.Table equals t.TableId
                    where rt.Restauarnt == restaurantId
                    select rt.Count*t.Size).Sum();
        }
    }
}
