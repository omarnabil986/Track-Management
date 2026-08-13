using TakweneTrackManagement.Domain.Contracts;

namespace TakweneTrackManagement.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedAndMigrateDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Tracker");

            await seeder.SeedDataAsync();

            return app;
        }
    }
}
