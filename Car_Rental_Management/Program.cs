using Car_Rental_Management.Data;
using Car_Rental_Management.Repository.Implement;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Implement;
using Car_Rental_Management.Service.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<IDriverService, DriverService>();
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IDriverRepository, DriverRepository>();
        builder.Services.AddDbContext<ApplicationDbcontext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Car")));


//Add services to the container.
        builder.Services.AddControllersWithViews();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Driver}/{action=CreateDriver}/{id?}");

        app.Run();
    }
}