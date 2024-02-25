using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

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

            
            List<Table> tables = _appDbContext.Tables.ToList();
            int size = reservation.Size;
            int num = 0;
            foreach (Table table in tables)
            {
                int availableTables = (from rtt in _appDbContext.RestaurantTables
                                       join t in _appDbContext.Tables on rtt.TableId equals t.TableId
                                       join rv in _appDbContext.Reservations on rtt.RestaurantId equals rv.Restaurant.RestaurantId
                                       join rvt in _appDbContext.ReservedTables on rv.ReservationId equals rvt.ReservationId
                                       where t.Size == table.Size && rv.Restaurant.RestaurantId == reservation.ReservationId
                                       group new { Capacity = rtt.Count, reservedTableCount = rvt.Count } by new { rtt.Count, reservedTableCount = rvt.Count } into g
                                       select g.Key.Count - g.Sum(x => x.reservedTableCount)).ToList().First();
                
                while(size > 0 && size > table.Size && (availableTables > 0 || availableTables == null))
                {
                    num++;
                    size-=table.Size;
                }
                _appDbContext.Database.ExecuteSqlRaw($"INSERT INTO \"RestaurantTables\"(\"ReservationId\",\"TableId\",\"Count\") VALUES ({reservation.ReservationId},{table.TableId},{num})");   
            }
            _appDbContext.SaveChanges();
            return Ok("Reservation saved!");
        }
    }
}
