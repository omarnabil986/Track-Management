using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Common;
using TakweneTrackManagement.Domain.Entities.TrackDistributions;

namespace TakweneTrackManagement.Domain.Entities.DigitalServiceProviders
{
    public class Dsp : BaseEntity<int>
    {

        public string Name { get; set; } = default!;

        public ICollection<TrackDistribution> TrackDistributions { get; set; }
    = [];

    }
}
