using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Entities.Artists;

namespace TakweneTrackManagement.Infrastructure.Data.Configurations
{
    internal class ArtistConfigurations : IEntityTypeConfiguration<Artist>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Artist> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100);
        }
    }
}
