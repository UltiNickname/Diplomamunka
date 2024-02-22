﻿using System;
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
            return Ok("Reservation saved!");
        }
    }
}
