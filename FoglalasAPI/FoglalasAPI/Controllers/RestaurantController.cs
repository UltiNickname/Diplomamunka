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
                        Opening = r.Opening,
                        Closing = r.Closing,
                        KitchenClosing = r.KitchenClosing,
                        Outdoor = r.Outdoor,
                        SeperateRoom = r.SeperateRoom,
                        FixedTables = r.FixedTables,
                        Menu = r.Menu,
                        AnimalFriendly = r.AnimalFriendly,
                        SzepKartyaAvailable = r.SzepKartyaAvailable
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

        [HttpGet]
        [Route("GetCurrentCapacity")]
        public int GetCurrentCapacity(int restaurantId, DateOnly date, TimeOnly start, TimeOnly end)
        {
            return GetCapacity(restaurantId) - (from rvt in _appDbContext.ReservedTables
                                                join t in _appDbContext.Tables on rvt.Table.TableId equals t.TableId
                                                join rv in _appDbContext.Reservations on rvt.ReservationId equals rv.ReservationId
                                                where rv.Restaurant.RestaurantId == restaurantId && rv.Date == date && (rv.StartTime < end && rv.FinishedTime > start)
                                                select rvt.Count * t.Size).Sum();
        }

        [HttpGet]
        [Route("SeperateRoom")]
        public bool IsSeperateRoomAvailable(int restaurantId, DateOnly date)
        {
            var seperateRoomReservations = (from rv in _appDbContext.Reservations
                                   where rv.Restaurant.RestaurantId == restaurantId && rv.Date == date && rv.SeperateRoom == true
                                   select rv.ReservationId).ToList();
            if (seperateRoomReservations.Count() > 0)
                return true;
            else return false;
        }

        [HttpGet]
        [Route("TableAvailable")]
        public bool IsTableSizeAvailable(int restaurantId, int size)
        {
            var tableSize = (from rt in _appDbContext.RestaurantTables
                             join t in _appDbContext.Tables on rt.Table.TableId equals t.TableId
                             where rt.RestaurantId == restaurantId && t.Size == size
                             select rt.Count).ToList();
            if (tableSize.Count() > 0)
                return true;
            else return false;
        }

        [HttpGet]
        [Route("TableCount")]
        public int TableSizeCount(int restaurantId, int size, DateOnly date, TimeOnly start, TimeOnly end)
        {
            return (from rt in _appDbContext.RestaurantTables
                    join t in _appDbContext.Tables on rt.Table.TableId equals t.TableId
                    where rt.RestaurantId == restaurantId && t.Size == size
                    select rt.Count).Sum() 
                    -
                    (from rvt in _appDbContext.ReservedTables
                    join t in _appDbContext.Tables on rvt.Table.TableId equals t.TableId
                    join rv in _appDbContext.Reservations on rvt.ReservationId equals rv.ReservationId
                    where rv.Restaurant.RestaurantId == restaurantId && rv.Date == date && (rv.StartTime < end && rv.FinishedTime > start)
                    select rvt.Count).Sum();
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
                Opening = restaurant.Opening,
                Closing = restaurant.Closing,
                KitchenClosing = restaurant.KitchenClosing,
                Outdoor = restaurant.Outdoor,
                SeperateRoom = restaurant.SeperateRoom,
                FixedTables = restaurant.FixedTables,
                Menu = restaurant.Menu,
                AnimalFriendly = restaurant.AnimalFriendly,
                SzepKartyaAvailable = restaurant.SzepKartyaAvailable
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
