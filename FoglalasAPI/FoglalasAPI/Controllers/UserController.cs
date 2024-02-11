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
    public class UserController : ControllerBase
    {
        private AppDbContext _appDbContext;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, AppDbContext context)
        {
            _appDbContext = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<User> GetAllUsers()
        {
            return (from u in _appDbContext.Users
                    select new User
                    {
                        Email = u.Email,
                        Username = u.Username,
                        Password = u.Password
                    }).ToList();
        }

        [HttpGet]
        [Route("Login/{userName}/{userPassword}")]
        public async Task<ActionResult<User>> Login(string userName, string userPassword) 
        {
            if((_appDbContext.Users == null))
            {
                return NotFound();
            }
            var userInfo = _appDbContext.Users.Where(u => u.Username == userName && u.Password == userPassword);
            if(userInfo == null)
            {
                return NotFound();
            }

            return Ok(userInfo);
        }

        [HttpGet]
        [Route("GetByEmail/{userEmail}")]
        public User GetUserByEmail(string userEmail)
        {
            return _appDbContext.Find<User>(userEmail);
        }

        [HttpPost]
        [Route("AddNewUser")]
        public async Task<IActionResult> SaveUser(User user)
        {
            if(_appDbContext.Users.Any(u => u.Email == user.Email)) 
            { 
                return BadRequest("User already exists."); 
            }
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return Ok("User created!");
        }
    }
}
