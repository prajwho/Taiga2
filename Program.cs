using BugTrackingSystem.Data;
using BugTrackingSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Adjust connection string

// Add Identity services with ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure cookie-based authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/auth/login"; // Redirect to login route
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure cookies are secure in production
    options.Cookie.SameSite = SameSiteMode.None;
});

// Add CORS configuration (adjust the allowed origin as needed)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Your Angular frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add controllers for API endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware
app.UseCors("AllowAngularApp"); // Enable CORS policy

app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable authorization

app.MapControllers(); // Map API routes

app.UseSwagger(); // Enable Swagger UI
app.UseSwaggerUI();

app.Run();
