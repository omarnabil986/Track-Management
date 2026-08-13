using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Entities.TrackDistributions;

namespace TakweneTrackManagement.Infrastructure.Data.Configurations
{
    internal class TrackDistributionConfigurations : IEntityTypeConfiguration<TrackDistribution>
    {
        public void Configure(EntityTypeBuilder<TrackDistribution> builder)
        {
            builder.HasOne(x => x.Track)
                  .WithMany(x => x.TrackDistributions)
                  .HasForeignKey(x => x.TrackId);

            builder.HasOne(x => x.Dsp)
                  .WithMany(x => x.TrackDistributions)
                  .HasForeignKey(x => x.DspId);

            builder.Property(x => x.Status)
              .HasConversion<string>();
        }
    }
}
