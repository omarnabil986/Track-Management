using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Entities.DigitalServiceProviders;
using TakweneTrackManagement.Domain.Entities.TrackDistributions;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Application.DTOs.TrackDistributions
{
    public class TrackDistributionDto
    {

        public int TrackId { get; set; }
      

        public int DspId { get; set; }
    

        public DateTime SubmittedAt { get; set; }

        public string Status { get; set; } = default!;
    }
}
