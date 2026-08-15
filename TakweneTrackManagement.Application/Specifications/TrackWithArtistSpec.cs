using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Entities.Artists;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Application.Specifications
{
    internal class TrackWithArtistSpec : BaseSpecification<Track, int>
    {
        public TrackWithArtistSpec(string? status, int? ArtistId, string? Genre) :
     base(t => (string.IsNullOrEmpty(status) ||
               t.Status == Enum.Parse<TrackStatus>(status!, true))
     && (ArtistId == null || t.ArtistId == ArtistId)
       && (Genre == null || t.Genre == Genre)
     )
        {
            AddInclude(t => t.Artist);
            AddInclude("TrackDistributions.Dsp");
        }

        public TrackWithArtistSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(t => t.Artist);
            AddInclude("TrackDistributions.Dsp");


        }
    }
}
