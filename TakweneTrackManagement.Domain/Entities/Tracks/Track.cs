using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Common;
using TakweneTrackManagement.Domain.Entities.Artists;
using TakweneTrackManagement.Domain.Entities.TrackDistributions;

namespace TakweneTrackManagement.Domain.Entities.Tracks
{
    public class Track : BaseEntity<int>
    {
        public string Title { get; set; } = default!;

        public string Isrc { get; set; } = default!;

        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; } = default!;
        public TrackStatus Status { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; } = default!;

        public ICollection<TrackDistribution> TrackDistributions { get; set; }
     = [];



    }
}
