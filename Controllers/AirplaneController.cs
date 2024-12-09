using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyPlanner.Context;
using SkyPlanner.Generics;
using SkyPlanner.Services.Response.Airplane;

namespace SkyPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AirplaneController : Controller
    {
        private readonly SkyPlannerDbContext _dbcontext;
        public AirplaneController(SkyPlannerDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<Collection<AirplaneTableDTO>>> GetAirplanes()
        {
            Collection<AirplaneTableDTO> response = new();

            var airplanes = await _dbcontext.Airplanes.Include(x => x.Status).Include(x => x.Airline)
                                            .Where(x => x.IsActive && x.Airline.IsActive)
                                            .ToListAsync();

            var query = from a in airplanes
                        orderby a.Model
                        select new AirplaneTableDTO
                        {
                            AirplaneId = a.AirplaneId,
                            Model = a.Model,
                            Capacity = int.Parse(a.Capacity),
                            MaxSpeed = a.MaxSpeed,
                            Status = a.Status.Name,
                            Airline = a.Airline.Name
                        };

            response.Data = query.ToList();
            response.Total = query.Count();

            return response;
        }

        [HttpGet("ByAirlineId")]
        public async Task<ActionResult<Collection<AirplaneTableDTO>>> GetAirplanesByAirline([FromQuery] Guid airlineId)
        {
            Collection<AirplaneTableDTO> response = new();

            if (airlineId == Guid.Empty)
            {
                return response;
            }

            var airplanes = await _dbcontext.Airplanes
                                            .Include(x => x.Status)
                                            .Include(x => x.Airline)
                                            .Where(x => x.IsActive && x.Airline.IsActive && x.AirlineId == airlineId)
                                            .ToListAsync();

            var query = from a in airplanes
                        orderby a.Model
                        select new AirplaneTableDTO
                        {
                            AirplaneId = a.AirplaneId,
                            Model = a.Model,
                            Capacity = int.Parse(a.Capacity),
                            MaxSpeed = a.MaxSpeed,
                            Status = a.Status.Name,
                            Airline = a.Airline.Name
                        };

            response.Data = query.ToList();
            response.Total = query.Count();

            return response;
        }

        [HttpGet("ByAirportId")]
        public async Task<ActionResult<AirplaneByAirportDTO>> GetAirplanesByAirport([FromQuery] Guid airportId)
        { 

            var airlines = await _dbcontext.AirlineAirports
                                           .Include(x => x.Airline)
                                           .Include(x => x.Airport)
                                           .Where(x => x.AirportId == airportId && x.Airport.IsActive && x.Airline.IsActive)
                                           .Distinct()
                                           .ToListAsync();
            
            var searchAirLines = airlines.Select(x => x.AirlineId)
                                         .Distinct()
                                         .ToList();

            var airplanes = await _dbcontext.Airplanes
                                    .Include(x => x.Airline)
                                    .Include(x => x.Status)
                                    .Where(x => searchAirLines.Contains(x.AirlineId))
                                    .ToListAsync();

            var query = new AirplaneByAirportDTO
            {
                Airport = airlines.First().Airport.Name,
                Airplanes = (from a in airplanes
                             orderby a.Model
                             select new AirplaneTableDTO
                             {
                                 AirplaneId = a.AirplaneId,
                                 Model = a.Model,
                                 Capacity = int.Parse(a.Capacity),
                                 MaxSpeed = a.MaxSpeed,
                                 Status = a.Status.Name,
                                 Airline = a.Airline.Name,
                             }).ToList()
            };

            return query;
        }

    }
}
