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

    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
            _userService = new UserService();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals("/products") && !context.Request.Method.Equals("POST"))
            {
                await _next(context);
                return;
            }

            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers.Authorization);
            if (authHeader.Scheme != "Basic")
            {
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
                context.Response.StatusCode = 404;
                return;
            }

            if (!isExists.Result.IsSeller)
            {
                context.Response.StatusCode = 401;
                return;
            }

            await _next(context);
        }
    }
}
