using FoglalasAPI.Context;
using FoglalasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoglalasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private AppDbContext _appDbContext;

        private readonly ILogger<CityController> _logger;

        public CityController(ILogger<CityController> logger, AppDbContext context)
        {
            _appDbContext = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<City> GetAllCities()
        {
            return (from c in _appDbContext.Cities
                    select new City
                    {
                        CityId = c.CityId,
                        Name = c.Name
                    }).ToList();
        }

        [HttpPost]
        [Route("AddNewCity")]
        public async Task<IActionResult> AddCity(City city)
        {
            if (_appDbContext.Cities.Any(c => c.Name == city.Name))
            {
                return BadRequest("City already exists.");
            }
            _appDbContext.Cities.Add(city);
            _appDbContext.SaveChanges();
            return Ok("City created!");
        }
    }
}
