using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Infra.Contexts;
using Domain.Services.GameServices;
using Domain.Services.UserServices;
using Domain.ViewModels;
using Domain.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    #region Home
    endpoints.MapGet("/", () => "Hello World!").WithTags("Home");
    #endregion

    #region Users
    endpoints.MapPost("/user", ([FromBody] UserDto userDto, IUserService userService) =>
    {
        try
        {
            var user = new User(userDto.Email, userDto.Password, userDto.Role);
            userService.CreateUser(user);

            var response = new SuccessfulCreateViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return Results.Created($"/user/{user.Id}", response);
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }

    }).WithTags("Users");

    endpoints.MapPut("/user/{id}", ([FromRoute]string id ,[FromBody] UpdateUserDto updateUserDto, IUserService userService) =>
    {
        try
        {
            var guidId = new Guid(id);
            var user = userService.GetUserById(guidId);

            if (user == null)
                return Results.NotFound(new ResultViewModel<SuccessfulCreateViewModel>("User not found"));

            userService.UpdateUser(user);
            var response = new SuccessfulCreateViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return Results.Ok(new ResultViewModel<SuccessfulCreateViewModel>(response));
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }

    }).WithTags("Users");

    endpoints.MapGet("user/{id}", ([FromRoute] string id, IUserService userService) =>
    {
        try
        {
            var guidId = new Guid(id);
            var user = userService.GetUserById(guidId);

            if (user == null)
                return Results.NotFound(new ResultViewModel<SuccessfulCreateViewModel>("User not found"));

            var response = new SuccessfulCreateViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return Results.Ok(new ResultViewModel<SuccessfulCreateViewModel>(response));
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }
    }).WithTags("Users");

    endpoints.MapDelete("user/{id}", ([FromRoute]string id, IUserService userService) =>
    {
        try
        {
            var guidId = new Guid(id);
            var user = userService.GetUserById(guidId);
            if (user == null)
                return Results.NotFound(new ResultViewModel<SuccessfulCreateViewModel>("User not found"));

            userService.DeleteUser(user);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }
    }).WithTags("Users");

    endpoints.MapGet("user/GetAll", (IUserService userService, [FromQuery]int? pagina, [FromQuery]int? quantidade) =>
    {
        try
        {
            var userList = userService.GetAllUsers(pagina, quantidade);
            if (userList.Count == 0)
                return Results.NotFound(new ResultViewModel<List<SuccessfulCreateViewModel>>("Users Not found"));

            var response = new List<SuccessfulCreateViewModel>();
            foreach(var user in userList)
            {
                response.Add(new SuccessfulCreateViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role.ToString()
                });
            }

            return Results.Ok(new ResultViewModel<List<SuccessfulCreateViewModel>>(response));

        }catch (Exception ex){
            return Results.StatusCode(500);
        }
    }).WithTags("Users");

    endpoints.MapGet("/user/getByRole", ([FromQuery] Role role, [FromQuery] int? pagina, [FromQuery] int? quantidade, IUserService userService) =>
    {
        try
        {
            var userList = userService.GetUsersByRole(role, pagina, quantidade);
            if (userList.Count == 0)
                return Results.NotFound(new ResultViewModel<List<SuccessfulCreateViewModel>>("Users Not found"));

            var response = new List<SuccessfulCreateViewModel>();
            foreach (var user in userList)
            {
                response.Add(new SuccessfulCreateViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role.ToString()
                });
            }

            return Results.Ok(new ResultViewModel<List<SuccessfulCreateViewModel>>(response));
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }
    }).WithTags("Users");


    #endregion

});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run();