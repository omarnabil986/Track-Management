using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.Common;
using TakweneTrackManagement.Application.Contracts;
using TakweneTrackManagement.Application.DTOs.Artists;
using TakweneTrackManagement.Application.DTOs.TrackDistributions;
using TakweneTrackManagement.Application.DTOs.Tracks;
using TakweneTrackManagement.Application.Specifications;
using TakweneTrackManagement.Domain.Contracts;
using TakweneTrackManagement.Domain.Entities.Artists;
using TakweneTrackManagement.Domain.Entities.TrackDistributions;
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

       

        public async Task<Result<TrackDto>> CreateTrackAsync(TrackDto trackDto, CancellationToken ct = default)
        {
            var track = _mapper.Map<Track>(trackDto);

            _unitOfWork.GetRepository<Track, int>().Add(track);

            var result = await _unitOfWork.SaveChangesAsync(ct);

            return result == 0 ? Result<TrackDto>.Fail(Error.Failure("TrackCreate.Failure", "Can Not Create Track"))
                : Result<TrackDto>.Ok(trackDto);
        }

        public async Task<Result<IReadOnlyList<TrackToReturnDto>>> GetAllTracksAsync(string? Status, int? ArtistId, string? Genre, CancellationToken ct = default)
        {
            var spec = new TrackWithArtistSpec(Status, ArtistId, Genre);
            var tracks = await _unitOfWork.GetRepository<Track, int>().GetAllAsync(spec);
            var data = _mapper.Map<IReadOnlyList<TrackToReturnDto>>(tracks);
            return Result<IReadOnlyList<TrackToReturnDto>>.Ok(data);
        }

        public async Task<Result<TrackToReturnDto>> GetTrackByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new TrackWithArtistSpec(id);

            var track = await _unitOfWork.GetRepository<Track, int>().GetByIdAsync(spec, ct);
            if (track == null)
                return Error.NotFound("Track.NotFound", $"Track with {id} is not found");

            return _mapper.Map<TrackToReturnDto>(track);
        }

        public async Task<Result<IReadOnlyList<TrackDistributionDto>>> DistributeTrackAsync(
         int trackId, DistributeTrackDto dto, CancellationToken ct = default)
        {
            var track = await _unitOfWork.GetRepository<Track, int>().GetByIdAsync(trackId, ct);
            if (track == null)
                return Error.NotFound("Track.NotFound", $"Track with id {trackId} is not found");

            var distributions = dto.DspIds.Select(dspId => new TrackDistribution
            {
                TrackId = trackId,
                DspId = dspId,
                Status = DistributionStatus.Pending,
                SubmittedAt = DateTime.UtcNow
            }).ToList();

            foreach (var d in distributions)
                _unitOfWork.GetRepository<TrackDistribution, int>().Add(d);

            await _unitOfWork.SaveChangesAsync(ct);
            var result = _mapper.Map<IReadOnlyList<TrackDistributionDto>>(distributions);

            return Result<IReadOnlyList<TrackDistributionDto>>.Ok(result);
        }

        public async Task<Result<TrackToReturnDto>> UpdateTrackStatusAsync(
        int id, UpdateTrackStatusDto dto, CancellationToken ct = default)
        {
            var spec = new TrackWithArtistSpec(id);
            var track = await _unitOfWork.GetRepository<Track, int>().GetByIdAsync(spec, ct);
            if (track == null)
                return Error.NotFound("Track.NotFound", $"Track with id {id} is not found");

            if (!Enum.TryParse<TrackStatus>(dto.Status, true, out var status))
                return Error.Validation("Track.InvalidStatus", $"Invalid status value: '{dto.Status}'");

            track.Status = status;
            _unitOfWork.GetRepository<Track, int>().Update(track);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<TrackToReturnDto>(track);
        }
    }
}
