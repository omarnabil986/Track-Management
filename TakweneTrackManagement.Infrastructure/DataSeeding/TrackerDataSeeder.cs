using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Common;
using TakweneTrackManagement.Domain.Contracts;
using TakweneTrackManagement.Domain.Entities.Artists;
using TakweneTrackManagement.Domain.Entities.DigitalServiceProviders;
using TakweneTrackManagement.Domain.Entities.TrackDistributions;
using TakweneTrackManagement.Domain.Entities.Tracks;
using TakweneTrackManagement.Infrastructure.Data;
using System.Text.Json.Serialization;

namespace TakweneTrackManagement.Infrastructure.DataSeeding
{
    internal class TrackerDataSeeder(TrackManagerDbContext dbContext, ILogger<TrackerDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                    await dbContext.Database.MigrateAsync(ct);

                var seedPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                await SeedIfEmptyAsync<Artist, int>(seedPath, "artists.json", ct);
                await SeedIfEmptyAsync<Dsp, int>(seedPath, "dsbs.json", ct);
                await SeedIfEmptyAsync<Track, int>(seedPath, "tracks.json", ct);
                await SeedIfEmptyAsync<TrackDistribution, int>(seedPath, "track-distributions.json", ct);

                var result = await dbContext.SaveChangesAsync(ct);
                if (result > 0)
                    logger.LogInformation($"{result} Rows Added");
                else
                    logger.LogInformation("Database Already Seeded");

            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;

            }
        }

        private async Task SeedIfEmptyAsync<T, TKey>(string rootPath, string fileName, CancellationToken ct) where T : BaseEntity<TKey>
        {
            if (await dbContext.Set<T>().AnyAsync())
            {
                logger.LogInformation("Table Already Has Data");
                return;
            }

            var filePath = Path.Combine(rootPath, fileName);

            if (!File.Exists(filePath))
            {
                logger.LogWarning($"File {fileName} Does Not Exist");
                return;
            }

            using var fileStream = File.OpenRead(filePath);

           
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options, ct);
            if (items?.Any() ?? false)
                dbContext.Set<T>().AddRange(items);


        }
    }
}
