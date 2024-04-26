using FluentValidation;
using Market.DAL;
using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Middleware;
using Market.Services;
using Market.UseCases.Validators.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IRepositoryContext, RepositoryContext>()
                .AddScoped<ICartsRepository, CartsRepository>()
                .AddScoped<IUsersRepository, UsersRepository>()
                .AddScoped<IProductsRepository, ProductsRepository>()
                .AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();

builder.Services.AddScoped<CheckAuthFilter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

//app.UseMiddleware<MyMiddleware>();
//app.UseMiddleware<MyMiddleware2>();

//app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();