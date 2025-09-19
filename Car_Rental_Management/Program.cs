using Car_Rental_Management.Data;
using Car_Rental_Management.Extra_Needs;
using Car_Rental_Management.Repository.Implement;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Implement;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Repositories & Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminLoginService, AdminLoginService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminrepository, Addminrepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IStaffservice, StaffService>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarMaintenanceRepository, CarMaintenanceRepository>();
builder.Services.AddScoped<ICarMaintenanceService, CarMaintenanceService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();



// DbContext
builder.Services.AddDbContext<ApplicationDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Car")));

// Controllers & Views
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Ensure SuperAdmin exists
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbcontext>();
    var superAdminCreator = new CreateSuperAdmin(context);
    await superAdminCreator.EnsureSuperAdminAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
