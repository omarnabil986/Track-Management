using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.Common;
using TakweneTrackManagement.Application.Contracts;
using TakweneTrackManagement.Application.DTOs.Artists;
using TakweneTrackManagement.Application.DTOs.Tracks;
using TakweneTrackManagement.Application.Specifications;
using TakweneTrackManagement.Domain.Contracts;
using TakweneTrackManagement.Domain.Entities.Artists;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Application.Services
{
    internal class ArtistService : IArtistService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ArtistDto>> CreateArtistAsync(ArtistDto artistDto, CancellationToken ct = default)
        {
            var artist = _mapper.Map<Artist>(artistDto);

        _unitOfWork.GetRepository<Artist, int>().Add(artist);

            var result = await _unitOfWork.SaveChangesAsync(ct);
      
            return result == 0 ? Result<ArtistDto>.Fail(Error.Failure("ArtistCreate.Failure", "Can Not Create Artist"))
                : Result<ArtistDto>.Ok(artistDto);
        }

        public async Task<Result<IReadOnlyList<ArtistDto>>> GetAllArtistsAsync( CancellationToken ct = default)
        {
            var artists = await _unitOfWork.GetRepository<Artist, int>().GetAllAsync();
            var data = _mapper.Map<IReadOnlyList<ArtistDto>>(artists);
            return Result<IReadOnlyList<ArtistDto>>.Ok(data);
        }
    }
}
