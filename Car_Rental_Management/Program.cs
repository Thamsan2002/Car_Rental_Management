using Car_Rental_Management.Data;
using Car_Rental_Management.Repository.Implement;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Implement;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.Extra_Needs;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Repositories & Services
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAdminLoginService, AdminLoginService>();
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<IDriverService, DriverService>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<IDriverRepository, DriverRepository>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddScoped<IAdminrepository, Addminrepository>();
        builder.Services.AddScoped<IAdminLoginService, AdminLoginService>();
        builder.Services.AddScoped<IBookingService, BookingService>();
        builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        builder.Services.AddScoped<IAdminDashBoardService, AdminDashBoardService>();
        builder.Services.AddScoped<IStaffRepository, StaffRepository>();
        builder.Services.AddScoped<IUserService, UserService>();



        // ✅ DbContext
        builder.Services.AddDbContext<ApplicationDbcontext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Car")));

        builder.Services.AddControllersWithViews();
        builder.Services.AddSession();
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        // 🔹 Ensure SuperAdmin exists
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbcontext>();
            var superAdminCreator = new CreateSuperAdmin(context);
            await superAdminCreator.EnsureSuperAdminAsync();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=AdminLogin}/{action=Login}/{id?}");

        app.Run();
    }
}
