
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.Contracts;
using TakweneTrackManagement.Domain.Contracts;
using TakweneTrackManagement.Infrastructure.Data;
using TakweneTrackManagement.Infrastructure.DataSeeding;
using TakweneTrackManagement.Infrastructure.Identity.Data;
using TakweneTrackManagement.Infrastructure.Identity.Entities;
using TakweneTrackManagement.Infrastructure.Identity.Services;
using TakweneTrackManagement.Infrastructure.Repositories;

namespace TakweneTrackManagement.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TrackManagerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<TrackManagementIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });



            services.AddKeyedScoped<IDataSeeder, TrackerDataSeeder>("Tracker");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
            services.AddScoped<IUnitOfWork, UnitOfWork>();

          services.AddIdentityCore<ApplicationUser>()
          .AddRoles<IdentityRole>()
          .AddEntityFrameworkStores<TrackManagementIdentityDbContext>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();



            var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>()
                ?? throw new InvalidOperationException("Jwt Settings Is Missing");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };


            });

            return services;
        }
    }
}
