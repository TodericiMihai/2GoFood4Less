using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Infrastructure;
using _2GoFood4Less.Server.Models.AuthObjects;
using _2GoFood4Less.Server.Services.CartServices;
using _2GoFood4Less.Server.Services.CartServices.CartCalcualtion;
using _2GoFood4Less.Server.Services.MenuServices;
using _2GoFood4Less.Server.Services.OrderService;
using _2GoFood4Less.Server.Services.RestaurantServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Add services
// ----------------------
builder.Services.AddControllers();
builder.Services.AddAuthorization();

// ----------------------
// Database
// ----------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

// ----------------------
// Scoped services
// ----------------------
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<MenuItemService>();
builder.Services.AddSingleton<TokenProvider>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ICartCalculator, CartCalculator>();

// ----------------------
// Identity
// ----------------------
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ----------------------
// Configure cookie options for APIs
// ----------------------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"message\":\"Unauthorized\"}");
        }

        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"message\":\"Forbidden\"}");
        }

        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

// ----------------------
// JWT Authentication
// ----------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };

    // Prevent redirect on unauthorized API calls
    o.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse(); // suppress default redirect
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"message\":\"Unauthorized\"}");
        }
    };
});

// ----------------------
// CORS
// ----------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostFront", policy =>
        policy.WithOrigins("http://localhost:8081")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

var app = builder.Build();

// ----------------------
// Middleware pipeline
// ----------------------
app.UseHttpsRedirection();

// Apply CORS BEFORE Authentication/Authorization
app.UseCors("AllowLocalhostFront");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
