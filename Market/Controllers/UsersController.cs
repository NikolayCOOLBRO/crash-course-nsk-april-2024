using FluentValidation;
using Market.DAL;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Middleware;
using Market.Misc;
using Market.Models;
using Market.Services;
using Market.UseCases.Exceptions;
using Market.UseCases.Validators.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Market.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IValidator<CreateUserDto> _validator;

        public UsersController()
        {
            _userService = new UserService();
            _validator = new CreateUserValidator();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var messages = new List<string>();

                foreach (var item in validationResult.Errors)
                {
                    messages.Add($"Property - {item.PropertyName}: {item.ErrorMessage}");
                }

                var errorDetails = new ErrorDetails()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Messages = messages
                };
                throw new InvalidDataRequestException(errorDetails);
            }

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
