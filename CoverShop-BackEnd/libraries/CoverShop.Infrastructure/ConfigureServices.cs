using CoverShop.Application.Contracts;
using CoverShop.Infrastructure.Contexts;
using CoverShop.Infrastructure.Identity.Adapters;
using CoverShop.Infrastructure.Identity.Adapters.Contracts;
using CoverShop.Infrastructure.Identity.Models;
using CoverShop.Infrastructure.Identity.Services;
using CoverShop.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoverShop.Infrastructure;

public static class ConfigureServices
{
    private const string connectionStringKey = "CoverShopDb";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey);

        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(connectionString));

        var jwtSetting = configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>()!;
        services.Configure<JwtSetting>(configuration.GetSection(nameof(JwtSetting)));

        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ValidIssuer = jwtSetting.Issuer,
            ValidAudience = jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey))
        };

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.RequireHttpsMetadata = true;
            o.TokenValidationParameters = tokenValidationParameters;
        });

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddUserManager<ApplicationUserManager>()
        .AddSignInManager<ApplicationSignInManager>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IUserManagerAdapter, UserManagerAdapter>();
        services.AddScoped<ISignInManagerAdapter, SignInManagerAdapter>();
        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
