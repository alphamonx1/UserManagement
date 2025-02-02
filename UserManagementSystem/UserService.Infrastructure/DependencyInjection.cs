using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserService.Application.Services;
using UserService.Infrastructure.Database;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure
{
    namespace UserService.Infrastructure
    {
        public static class DependencyInjection
        {
            public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddDbContext<UserDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                services.AddScoped<IUserService,AuthService>();

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]))
                    };
                });
                services.AddAuthorization();

                return services;
            }
        }
    }
}
