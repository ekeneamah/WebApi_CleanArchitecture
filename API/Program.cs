using Application.Interfaces;
using Application.Interfaces.Authentication;
using Application.Interfaces.Content.Brands;
using Application.Interfaces.Content.Categories;
using Application.Interfaces.Content.Claim;
using Application.Interfaces.Content.Policy;
using Application.Interfaces.Content.Products;
using Application.Interfaces.Content.UserProfiles;
using Domain.Settings;
using Infrastructure;
using Infrastructure.Content.Data;
using Infrastructure.Content.Services;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Seeds;
using Infrastructure.Identity.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using System.Collections;
using System.Text;
using System.Text.Json.Serialization;
using API.Filters;
using Application.Mapping;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//var connectionString2 = builder.Configuration.GetConnectionString("IdentityConnection");
builder.Configuration.AddEnvironmentVariables();
var ev = Environment.GetEnvironmentVariable("SQLCONNSTR_TranscapeMVP");
var connectionString = ev != null ? ev.Replace("\"", "").Replace("\\\\", "\\") : "environment is null";// builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

 builder.Services.AddDbContext<AppIdentityContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.UseHttpClientMetrics();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
})

    .AddEntityFrameworkStores<AppIdentityContext>()
    .AddDefaultTokenProviders();
builder.Services.AddHttpClient(); // UpdateUser HttpClient and IHttpClientFactory
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressInferBindingSourcesForParameters = true;
});

builder.Services.AddScoped<IInsuranceCoy, InsuranceCoyService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IClaim, ClaimService>();
builder.Services.AddScoped<IPolicy, PolicyService>();
builder.Services.AddScoped<IUserProfile, UserProfileService>();
builder.Services.AddScoped<ITransaction, TransactionService>();
builder.Services.AddScoped<IVehiclePremiumRepository, VehiclePremiumService>();
builder.Services.AddScoped<IMotorClaimRepository, MotorClaimService>();
builder.Services.AddScoped<ICategoryandInsurancecoy, CategoryandInsurancecoyService>();


builder.Services.AddScoped<IAuthResponse, AuthResponseService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IKYC, KYCService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(b =>
    {
        b.RequireHttpsMetadata = false;
        b.SaveToken = false;
        b.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.FromMinutes(1500)
            
            
        };
    });


// UpdateUser services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidatorActionFilter));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Other Swagger configurations...
    c.DescribeAllParametersInCamelCase();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    IDictionary environmentVariables = Environment.GetEnvironmentVariables();
    foreach (DictionaryEntry entry in environmentVariables)
    {
        Console.WriteLine($"{entry.Key} = {entry.Value}");
    }
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var identityDbContext = serviceProvider.GetRequiredService<AppIdentityContext>();
       // dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate(); // This line adds any pending migrations
        identityDbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}
using (IServiceScope? scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = service.GetRequiredService<AppIdentityContext>();
        var userManager = service.GetRequiredService<UserManager<AppUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
       await DefaultRoles.SeedRoles(roleManager);
        await DefaultUsers.SeedUsers(userManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
app.UseStaticFiles();
//}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.UseMetricServer();
app.UseHttpMetrics();
app.MapControllers();

app.Run();