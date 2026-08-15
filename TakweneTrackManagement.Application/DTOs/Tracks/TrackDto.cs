using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Entities.Artists;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Application.DTOs.Tracks
{
    public class TrackDto
    {
     
        public string Title { get; set; } = default!;
        public string Isrc { get; set; } = default!;
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; } = default!;
        public string Status { get; set; } = default!;
        public int ArtistId { get; set; }


    }
}
