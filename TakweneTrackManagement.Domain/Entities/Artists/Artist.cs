using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Common;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Domain.Entities.Artists
{
    public class Artist : BaseEntity<int>
    {
        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Country { get; set; } = default!;
        public ICollection<Track> Tracks { get; set; } = [];
    }
}
