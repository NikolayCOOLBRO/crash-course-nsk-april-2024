using Market.Misc;
using Market.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http.Headers;
using System.Text;

namespace Market.Middleware
{
    public class CheckAuthFilter : ActionFilterAttribute
    {
        private readonly IUserService _userService;

        public CheckAuthFilter()
        {
            _userService = new UserService();
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authHeader = AuthenticationHeaderValue.Parse(context.HttpContext.Request.Headers.Authorization);
            if (authHeader.Scheme != "Basic")
            {
                context.Result = new StatusCodeResult(404);
                return;
            }

            if (string.IsNullOrEmpty(authHeader.Parameter))
            {
                return;
            }

            var creditionalsBytes = Convert.FromBase64String(authHeader.Parameter);
            var rawCreadentials = Encoding.UTF8.GetString(creditionalsBytes);
            var credential = rawCreadentials.Split(':');
            var logIn = credential[0];
            var password = credential[1];

            var isExists = await _userService.IsUserExists(logIn, password);

            if (!DbResultHelper.DbResultIsSuccessful(isExists, out var error))
            {
                context.Result = new StatusCodeResult(404);
                return;
            }

            if (!isExists.Result.IsSeller)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            await next();
        }
    }
}
