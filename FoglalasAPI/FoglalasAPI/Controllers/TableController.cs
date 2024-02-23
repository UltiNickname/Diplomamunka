using FoglalasAPI.Context;
using FoglalasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoglalasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private AppDbContext _appDbContext;

        private readonly ILogger<TableController> _logger;

        public TableController(ILogger<TableController> logger, AppDbContext context)
        {
            _appDbContext = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Table> GetAllTables()
        {
            return (from t in _appDbContext.Tables
                    select new Table
                    {
                        TableId = t.TableId,
                        Size = t.Size,
                    }).ToList();
        }
    }
}
