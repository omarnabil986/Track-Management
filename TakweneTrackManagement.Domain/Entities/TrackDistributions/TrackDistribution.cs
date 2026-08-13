using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Common;
using TakweneTrackManagement.Domain.Entities.DigitalServiceProviders;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Domain.Entities.TrackDistributions
{
    public class TrackDistribution : BaseEntity<int>
    {
        public int TrackId { get; set; }
        public Track Track { get; set; } = default!;

        public int DspId { get; set; }
        public Dsp Dsp { get; set; } = default!;

        public DateTime SubmittedAt { get; set; }

        public DistributionStatus Status { get; set; }
    }
}
