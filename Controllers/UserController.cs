using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyPlanner.Context;
using SkyPlanner.Entities;
using SkyPlanner.Generics;
using SkyPlanner.Helpers;
using SkyPlanner.Services.Response;

namespace SkyPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DateHelper _timeHelper = new();
        private readonly SkyPlannerDbContext _dbContext;

        public UserController(
            SkyPlannerDbContext dbContext    
        )
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Collection<UserTableDTO>>> UserTable()
        {
            Collection<UserTableDTO> response = new();


            var users = await _dbContext.UserCredentials
                                .Include(x => x.User)
                                .Where(x => x.IsActive && x.User.IsActive)
                                .ToListAsync();

            var query = from u in users
                        orderby u.User.FirstName
                        select new UserTableDTO
                        {
                            UserId = u.UserId,
                            FirstName = u.User.FirstName,
                            LastName = u.User.LastName,
                            Email = u.User.Email,
                            UserName = u.UserName,
                            MemberShipDate = _timeHelper.ConvertDateToString(u.User.MemberShipDate)
                        };

            response.Total = query.Count();
            response.Data = query.ToList();

            return response;
        }
    }
}
