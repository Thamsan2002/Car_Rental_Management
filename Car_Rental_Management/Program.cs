using Car_Rental_Management.Data;
using Car_Rental_Management.Repository.Implement;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Implement;
using Car_Rental_Management.Service.Interface;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ✅ Add Repositories & Services
        builder.Services.AddScoped<IDriverService, DriverService>();
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IDriverRepository, DriverRepository>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddScoped<IAdminrepository, Addminrepository>();
        builder.Services.AddScoped<IAdminLoginService, AdminLoginService>();
        builder.Services.AddScoped<IBookingService, BookingService>();
        builder.Services.AddScoped<IBookingRepository, BookingRepository>();


        // ✅ DbContext
        builder.Services.AddDbContext<ApplicationDbcontext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Car")));

        // ✅ Controllers with views
        builder.Services.AddControllersWithViews();

        // ✅ Session
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // ✅ IHttpContextAccessor
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // ✅ Authentication & Authorization (if needed)
        app.UseAuthentication();
        app.UseAuthorization();

        // ✅ Session middleware
        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
