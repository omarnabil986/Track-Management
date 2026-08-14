using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Application.Common;
using TakweneTrackManagement.Application.DTOs.Artists;
using TakweneTrackManagement.Application.DTOs.Tracks;

namespace TakweneTrackManagement.Application.Contracts
{
    public interface IArtistService
    {
        Task<Result<ArtistDto>> CreateArtistAsync(ArtistDto orderDto, CancellationToken ct = default);
        Task<Result<IReadOnlyList<ArtistDto>>> GetAllArtistsAsync( CancellationToken ct = default);
       
    }
}
