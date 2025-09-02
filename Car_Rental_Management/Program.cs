using Car_Rental_Management.Data;
using Car_Rental_Management.Intrface;
using Car_Rental_Management.Repositories;
using Car_Rental_Management.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<IStaffservice, StaffService>(); // Service interface ? Service class
        builder.Services.AddScoped<IUserRepository, UserRepository>(); // User repo
        builder.Services.AddScoped<IStaffRepository, StaffRepository>(); // Staff repo


        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<Db>(options
                        => options.UseSqlServer(builder.Configuration.GetConnectionString("Car")));


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
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}