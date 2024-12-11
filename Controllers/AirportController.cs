using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyPlanner.Context;
using SkyPlanner.Generics;
using SkyPlanner.Helpers;
using SkyPlanner.Services.Response.Airport;

namespace SkyPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly DateHelper _date = new();
        private readonly SkyPlannerDbContext _dbContetx;

        public AirportController(SkyPlannerDbContext dbContetx)
        {
            _dbContetx = dbContetx;
        }

        [HttpGet]
        public async Task<ActionResult<Collection<AirportTableDTO>>> GetAirports()
        {
            Collection<AirportTableDTO> response = new();
            
            var airports = await _dbContetx.Airports
                                          .Include(x => x.City.Country)
                                          .Where(x => x.IsActive)
                                          .ToListAsync();

            var query = from a in airports
                        orderby a.Name
                        select new AirportTableDTO
                        {
                            AirportId = a.AirportId,
                            City = a.City.Name,
                            Name = a.Name,
                            Country = a.City.Country.Name,
                            FoundedDate = _date.ConvertDateToString(a.FoundedDate)
                        };

            response.Data = query.ToList();
            response.Total = query.Count();

            return response;
        }
    }
}
