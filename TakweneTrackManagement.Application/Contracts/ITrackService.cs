using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.Common;
using TakweneTrackManagement.Application.DTOs.Tracks;

namespace TakweneTrackManagement.Application.Contracts
{
    public interface ITrackService
    {
        Task<Result<IReadOnlyList<TrackDto>>> GetAllTracksAsync(string? Status, int? ArtistId, string? Genre, CancellationToken ct = default);
        Task<Result<TrackDto>> GetTrackByIdAsync(int id, CancellationToken ct = default);
    }
}
