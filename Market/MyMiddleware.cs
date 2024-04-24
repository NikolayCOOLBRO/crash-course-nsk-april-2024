using Market.DAL.Repositories;

internal class MyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly CartsRepository _cartsRepository;

    public MyMiddleware(RequestDelegate next)
    {
        _next = next;
        _cartsRepository = new CartsRepository();
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add("ffff", "ffffff");
        await _next(context);
    }
}

internal class MyMiddleware2
{
    private readonly RequestDelegate _next;
    private readonly CartsRepository _cartsRepository;

    public MyMiddleware2(RequestDelegate next)
    {
        _next = next;
        _cartsRepository = new CartsRepository();
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add("aaaa", "aaaaaaaa");
        await _next(context);
    }
}