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

        public bool ReservationRepeated(Reservation reservation)
        {
            return _appDbContext.Reservations.Where(r => r.Equals(reservation)).Any();
        }

        [HttpPost]
        [Route("AddNewReservation")]
        public async Task<IActionResult> SaveReservation(Reservation reservation)
        {
            if (ReservationRepeated(reservation))
                return BadRequest("This reservation is already exist");
            _appDbContext.Reservations.Add(reservation);
            _appDbContext.SaveChanges();
            return Ok("Reservation saved!");
        }
    }
}
