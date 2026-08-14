using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Application.DTOs.Tracks
{
    public class TrackToReturnDto
    {
        public string ArtistName { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
