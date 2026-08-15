using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.DTOs.Artists;
using TakweneTrackManagement.Application.DTOs.Tracks;
using TakweneTrackManagement.Domain.Entities.Artists;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Application.Profiles
{
    internal class ArtistProfile : Profile
    {
        public ArtistProfile()
        {

            CreateMap<Artist, ArtistDto>().ReverseMap();

        }
    }
}
