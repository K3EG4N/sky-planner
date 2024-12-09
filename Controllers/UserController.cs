using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyPlanner.Context;
using SkyPlanner.Generics;
using SkyPlanner.Helpers;
using SkyPlanner.Services.Response.User;

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
                                .Include(x => x.User.UserType)
                                .Where(x => x.IsActive && x.User.IsActive)
                                .ToListAsync();

            var userRoles = await _dbContext.UserRoles
                                    .Include(x => x.Role)
                                    .ToListAsync();

            var query = from u in users
                        join ur in userRoles on u.UserId equals ur.UserId
                        group ur.Role.Name by u into groupedRoles
                        orderby groupedRoles.Key.User.FirstName
                        select new UserTableDTO
                        {
                            UserId = groupedRoles.Key.UserId,
                            UserType = groupedRoles.Key.User.UserType?.Name ?? "",
                            FirstName = groupedRoles.Key.User.FirstName,
                            LastName = groupedRoles.Key.User.LastName,
                            Email = groupedRoles.Key.User.Email,
                            UserName = groupedRoles.Key.UserName,
                            MemberShipDate = _timeHelper.ConvertDateToString(groupedRoles.Key.User.MemberShipDate),
                            Roles = [.. groupedRoles]
                        };

            response.Data = query.ToList();
            response.Total = query.Count();

            return response;
        }
    }
}
