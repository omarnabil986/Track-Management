using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Infrastructure.Data.Configurations
{
    internal class TrackConfigurations : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasOne(x => x.Artist)
                   .WithMany(x => x.Tracks)
                   .HasForeignKey(x => x.ArtistId);

            builder.Property(x => x.Status)
            .HasConversion<string>();

            builder.HasIndex(x => x.Isrc)
         .IsUnique();

        }
    }
}
