using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyPlanner.Context;
using SkyPlanner.Entities;
using SkyPlanner.Generics;
using SkyPlanner.Services.Request;
using SkyPlanner.Utilities;

namespace SkyPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly SkyPlannerDbContext _dbContext;
        private readonly TokenConfiguration _token;

        public Authentication(
            SkyPlannerDbContext dbContext,
            TokenConfiguration token
        )
        {
            _dbContext = dbContext;
            _token = token;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginBody request)
        {
            LoginResponse response = new ();

            try
            {
                if (string.IsNullOrEmpty(request.Username))
                {
                    response.Message = "Username cannot be empty";
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(request.Password))
                {
                    response.Message = "Password cannot be empty";
                    return BadRequest(response);
                }

                var user = await _dbContext.UserCredentials
                                 .Include(x => x.User)
                                 .FirstOrDefaultAsync(x => x.UserName.Equals(request.Username));

                if (user == null)
                {
                    response.Message = "Username or Password incorrect";
                    return Unauthorized(response);
                }

                if(!_token.VerifyPassword(request.Password, user.Password))
                {
                    response.Message = "Username or Password incorrect";
                    return Unauthorized(response);
                }

                var token = _token.JWTgenerate(user.User);

                response.Message = "Login succesfull";
                response.Success = true;
                response.Token = token;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseBase>> RegisterAccount([FromBody] RegisterBody body)
        {
            ResponseBase response = new();

            if (string.IsNullOrEmpty(body.Email))
            {
                response.Message = "No user content";
                return BadRequest(response);
            }

            if (string.IsNullOrEmpty(body.Firstname))
            {
                response.Message = "No firstname content";
                return BadRequest(response);
            }

            if (string.IsNullOrEmpty(body.Lastname))
            {
                response.Message = "No firstname content";
                return BadRequest(response);
            }

            string generatePassword = string.Concat(body.Firstname.AsSpan(0, 2), body.Lastname.AsSpan(0, 3)) + DateTime.Now.Year;
            Guid userType = (await _dbContext.UserTypes.FirstAsync(x => x.Code == USER_TYPE_ENUM.CLIENT)).UserTypeId;

            Guid newUserId = Guid.NewGuid();

            Guid rolUser = (await _dbContext.Roles.FirstAsync(x => x.Code == ROLE_ENUM.USER)).RoleId;

            User newUser = new()
            {
                UserId = newUserId,
                Email = body.Email,
                FirstName = body.Firstname,
                LastName = body.Lastname,
                MemberShipDate = DateTime.Now,
                UserTypeId = userType,
            };

            UserCredential newCredential = new()
            {
                UserId = newUserId,
                Password = _token.HashPassword(generatePassword),
                UserName = body.Email
            };

            UserRole newRole = new() { RoleId = rolUser, UserId = newUserId, AssignedAt = DateTime.Now };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.UserCredentials.AddAsync(newCredential);
            await _dbContext.UserRoles.AddAsync(newRole);
            _dbContext.SaveChanges();

            response.Message = "User created";
            response.Success = true;

            return response;
        }
    }
}
