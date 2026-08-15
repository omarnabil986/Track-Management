using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Application.DTOs.Artists
{
    public class ArtistDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Country { get; set; } = default!;
        public ICollection<Track> Tracks { get; set; } = [];
    }
}
