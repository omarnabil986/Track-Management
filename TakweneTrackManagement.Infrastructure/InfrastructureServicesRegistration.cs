using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Contracts;
using TakweneTrackManagement.Infrastructure.Data;
using TakweneTrackManagement.Infrastructure.DataSeeding;
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

            services.AddKeyedScoped<IDataSeeder, TrackerDataSeeder>("Tracker");
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
