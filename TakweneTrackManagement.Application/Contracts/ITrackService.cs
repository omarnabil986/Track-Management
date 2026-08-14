using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.Common;
using TakweneTrackManagement.Application.DTOs.Artists;
using TakweneTrackManagement.Application.DTOs.TrackDistributions;
using TakweneTrackManagement.Application.DTOs.Tracks;

namespace TakweneTrackManagement.Application.Contracts
{
    public interface ITrackService
    {
        Task<Result<TrackDto>> CreateTrackAsync(TrackDto trackDto, CancellationToken ct = default);

        Task<Result<IReadOnlyList<TrackToReturnDto>>> GetAllTracksAsync(string? Status, int? ArtistId, string? Genre, CancellationToken ct = default);
        Task<Result<TrackToReturnDto>> GetTrackByIdAsync(int id, CancellationToken ct = default);

        Task<Result<IReadOnlyList<TrackDistributionDto>>> DistributeTrackAsync(int trackId, DistributeTrackDto dto, CancellationToken ct = default);
    }
}
