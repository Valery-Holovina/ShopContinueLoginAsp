
//dotnet new web -n MyWebApp && cd MyWebApp && dotnet run
//dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL && dotnet add package Microsoft.EntityFrameworkCore.Tools
// dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
// dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.0


//dotnet clean
//dotnet build

//dotnet ef migrations add InitialCreate
//dotnet ef database update

// dotnet ef migrations add SecondCreate
// dotnet ef database update



//dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
//dotnet add package Microsoft.AspNetCore.Identity.UI

using Microsoft.EntityFrameworkCore;
using Shop_P41.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

// =========================
// DATABASE (PostgreSQL)
// =========================
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =========================
// IDENTITY (WITH ROLES)
// =========================
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;

    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ShopContext>()
.AddDefaultTokenProviders();

// JWT configuration
var my_token_password = "K9#mP2$vL8@qR5!xT3&nW7^bC4*zH6!a";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "http://localhost:5012",
        ValidAudience = "http://localhost:5012",

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(my_token_password)
        ),

        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name
    };
});

builder.Services.AddAuthorization();


builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "ShopApp.Auth";
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                options.SlidingExpiration = true;

                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Denied";
            });

// =========================
// SERVICES
// =========================
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// =========================
// MIDDLEWARE
// =========================
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// =========================
// ROUTES
// =========================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}"
);


//------------
// // Generate a password hash
// var hasher = new PasswordHasher<IdentityUser>();

// var user = new IdentityUser
// {
//     UserName = "alex@gmail.com",
//     Email = "alex@gmail.com"
// };

// var hash = hasher.HashPassword(user, "Test123!");

// Console.WriteLine(hash);

//SELECT * FROM "AspNetUsers"
// INSERT INTO "AspNetUsers" (
//     "Id",
//     "UserName",
//     "NormalizedUserName",
//     "Email",
//     "NormalizedEmail",
//     "EmailConfirmed",
//     "PasswordHash",
//     "SecurityStamp",
//     "ConcurrencyStamp",
//     "PhoneNumberConfirmed",
//     "TwoFactorEnabled",
//     "LockoutEnabled",
//     "AccessFailedCount"
// )
// VALUES (
//     gen_random_uuid()::text,
//     'alex@gmail.com',
//     'ALEX@GMAIL.COM',
//     'alex@gmail.com',
//     'ALEX@GMAIL.COM',
//     TRUE,
//     'PASTE_GENERATED_HASH_HERE',
//     gen_random_uuid()::text,
//     gen_random_uuid()::text,
//     FALSE,
//     FALSE,
//     TRUE,
//     0
// );

// UPDATE "AspNetUsers"
// SET "PasswordHash" = ''
// WHERE "Email" = 'alex@gmail.com';
// Test123! password
//---------------

app.Run();