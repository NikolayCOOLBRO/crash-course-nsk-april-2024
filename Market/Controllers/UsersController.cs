using FluentValidation;
using Market.DAL;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Middleware;
using Market.Misc;
using Market.Models;
using Market.Services.Users;
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

        private readonly IValidator<CreateUserDto> _validatorCreateUser;

        public UsersController(IUserService userServices, IValidator<CreateUserDto> validator)
        {
            _userService = userServices;
            _validatorCreateUser = validator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var validationResult = await _validatorCreateUser.ValidateAsync(dto);

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

            //if (_userService.IsUserExists(dto.Login, dto.Password).Result != null)
            //{
            //    return new StatusCodeResult(StatusCodes.Status409Conflict);
            //}

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
