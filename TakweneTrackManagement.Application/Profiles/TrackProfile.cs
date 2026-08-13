using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.DTOs.Tracks;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Application.Profiles
{
    internal class TrackProfile : Profile
    {
        public TrackProfile()
        {

            CreateMap<Track, TrackDto>()
                .ForMember(dst => dst.ArtistName, opt => opt.MapFrom(src => src.Artist.Name));
               
      
        }
    }
}
