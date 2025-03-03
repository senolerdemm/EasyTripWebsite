using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EasyTripProject.Models;
using Serilog;

// Add before var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext configuration
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<Admin, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
})
.AddEntityFrameworkStores<Context>()
.AddDefaultTokenProviders();

// Authentication configuration
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Login/Login";
    options.LogoutPath = "/Login/Logout";
    options.AccessDeniedPath = "/Login/AccessDenied";
    options.Cookie.Name = "EasyTripAuth";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Apply migrations BEFORE trying to create the admin user
using (var scope = app.Services.CreateScope()) {
    try {
        var context = scope.ServiceProvider.GetRequiredService<Context>();
        // Apply migrations programmatically
        context.Database.Migrate();
        
        // After migrations are applied, then create the admin user
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Admin>>();
        await CreateAdminUser(userManager);
    }
    catch (Exception ex) {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database migration or admin user creation");
    }
}

app.Run();

// Admin user creation method
static async Task CreateAdminUser(UserManager<Admin> userManager) {
    var user = await userManager.FindByNameAsync("senol");
    if (user == null) {
        var admin = new Admin {
            UserName = "senol",
            Email = "admin@easytrip.com",
            EmailConfirmed = true
        };
        
        var result = await userManager.CreateAsync(admin, "erdem");
        if (!result.Succeeded) {
            throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}