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
            foreach (Table table in tables)
            {
                if(table.Size <= size)
            }
            _appDbContext.SaveChanges();
            return Ok("Reservation saved!");
        }
    }
}
