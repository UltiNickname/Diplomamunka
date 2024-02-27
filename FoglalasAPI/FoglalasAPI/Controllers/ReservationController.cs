using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FoglalasAPI.Context;
using FoglalasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using Table = FoglalasAPI.Models.Table;

namespace FoglalasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private AppDbContext _appDbContext;

        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger, AppDbContext context)
        {
            _appDbContext = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Reservation> GetAllReservations()
        {
            return (from r in _appDbContext.Reservations
                    select new Reservation
                    {
                        ReservationId = r.ReservationId,
                        Restaurant = r.Restaurant,
                        User = r.User,
                        Size = r.Size,
                        Date = r.Date,
                        StartTime = r.StartTime,
                        FinishedTime = r.FinishedTime,
                        Outdoor = r.Outdoor,
                        SeperateRoom = r.SeperateRoom
                    }).ToList();
        }

        [HttpGet]
        [Route("GetUserAll")]
        public IEnumerable<Reservation> GetUserAllReservations(User user)
        {
            return (from r in _appDbContext.Reservations where r.User.UserId == user.UserId
                    select new Reservation
                    {
                        ReservationId = r.ReservationId,
                        Restaurant = r.Restaurant,
                        User = r.User,
                        Size = r.Size,
                        Date = r.Date,
                        StartTime = r.StartTime,
                        FinishedTime = r.FinishedTime,
                        Outdoor = r.Outdoor,
                        SeperateRoom = r.SeperateRoom
                    }).ToList();
        }

        [HttpPost]
        [Route("AddNewReservation")]
        public async Task<IActionResult> SaveReservation(Reservation reservation)
        {
            Reservation dbReservation = new Reservation()
            {
                Restaurant = _appDbContext.Restaurants.Find(reservation.Restaurant.RestaurantId),
                User = _appDbContext.Users.Find(reservation.User.UserId),
                Size = reservation.Size,
                Date = reservation.Date,
                StartTime = reservation.StartTime,
                FinishedTime = reservation.FinishedTime,
                Outdoor = reservation.Outdoor,
                SeperateRoom = reservation.SeperateRoom,
            };
            _appDbContext.Reservations.Add(dbReservation);
            _appDbContext.SaveChanges();
            int size = reservation.Size;
            List<Table> tables = _appDbContext.Tables.ToList();
            tables.Reverse();
            int num;
            do
            {
                foreach (Table table in tables)
                {
                    num = 0;
                    var availableTables = (from rtt in _appDbContext.RestaurantTables
                                           join t in _appDbContext.Tables on rtt.TableId equals t.TableId
                                           join rv in _appDbContext.Reservations on rtt.RestaurantId equals rv.Restaurant.RestaurantId
                                           join rvt in _appDbContext.ReservedTables on rv.ReservationId equals rvt.ReservationId
                                           where t.TableId == table.TableId && rtt.TableId == table.TableId && rtt.RestaurantId == rv.Restaurant.RestaurantId && rvt.TableId == table.TableId && rtt.RestaurantId == reservation.Restaurant.RestaurantId
                                           group new { RestaurantTableCount = rtt.Count, ReservedTableCount = rvt.Count }
                                           by new { restaurantTableCount = rtt.Count, reservedTableCount = rvt.Count } into g
                                           orderby g.Key.restaurantTableCount descending
                                           select g.Key.restaurantTableCount - g.Sum(x => x.ReservedTableCount)).ToList();

                    while (size > 0 && size >= table.Size && (availableTables.Count() == 0 || availableTables.First() > 0))
                    {
                        num++;
                        size -= table.Size;
                    }
                    if (num > 0)
                        _appDbContext.Database.ExecuteSqlRaw($"INSERT INTO \"ReservedTables\"(\"ReservationId\",\"TableId\",\"Count\") VALUES ({dbReservation.ReservationId},{table.TableId},{num})");
                }
                if (size > 0)
                    size += 2;
            } while (size > 0);
            
            _appDbContext.SaveChanges();
            return Ok("Reservation saved!");
        }
    }
}
