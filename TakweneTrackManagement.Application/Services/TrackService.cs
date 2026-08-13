using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.Common;
using TakweneTrackManagement.Application.Contracts;
using TakweneTrackManagement.Application.DTOs.Tracks;
using TakweneTrackManagement.Domain.Contracts;
using TakweneTrackManagement.Domain.Entities.Tracks;

namespace TakweneTrackManagement.Application.Services
{
    internal class TrackService : ITrackService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<TrackDto>>> GetAllTracksAsync(CancellationToken ct = default)
        {
            var tracks = await _unitOfWork.GetRepository<Track, int>().GetAllAsync(ct);
            var data = _mapper.Map<IReadOnlyList<TrackDto>>(tracks);
            return Result<IReadOnlyList<TrackDto>>.Ok(data);
        }

        public async Task<Result<TrackDto>> GetTrackByIdAsync(int id, CancellationToken ct = default)
        {
         
            var track = await _unitOfWork.GetRepository<Track, int>().GetByIdAsync(id, ct);
            if (track == null)
                return Error.NotFound("Track.NotFound", $"Track with {id} is not found");

            return _mapper.Map<TrackDto>(track);
        }
    }
}
