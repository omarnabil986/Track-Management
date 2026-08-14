using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.DTOs.TrackDistributions;
using TakweneTrackManagement.Domain.Entities.TrackDistributions;

namespace TakweneTrackManagement.Application.Profiles
{
    internal class TrackDistributionProfile  :Profile
    {
        public TrackDistributionProfile()
        {
            CreateMap<TrackDistribution, TrackDistributionDto>();
        }
    }
}
