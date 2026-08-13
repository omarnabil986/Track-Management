using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Entities.Artists;
using TakweneTrackManagement.Domain.Entities.DigitalServiceProviders;
using TakweneTrackManagement.Domain.Entities.TrackDistributions;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Infrastructure.Data
{
    internal class TrackManagerDbContext(DbContextOptions<TrackManagerDbContext> options) : DbContext(options)
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Dsp> Dsps   { get; set; }
        public DbSet<TrackDistribution> TrackDistributions   { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrackManagerDbContext).Assembly);
        }
    }
}
