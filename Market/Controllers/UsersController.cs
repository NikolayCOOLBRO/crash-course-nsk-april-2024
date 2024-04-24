using Market.DAL;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Middleware;
using Market.Misc;
using Market.Models;
using Market.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Market.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController()
        {
            _userService = new UserService();
        }

        [HttpPost]
        [CheckAuthFilter]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var result = await _userService.CreateUser(dto);

            return DbResultHelper.DbResultIsSuccessful(result, out var error)
                ? Ok(result)
                : error;
        }

        [HttpPatch("{customerId}/add-seller")]
        public async Task<IActionResult> SetSeller([FromRoute] Guid customerId)
        {
            var result = await _userService.SetUserSeller(customerId, true);

            return DbResultHelper.DbResultIsSuccessful(result, out var error)
                ? Ok()
                : error;
        }
    }
}
