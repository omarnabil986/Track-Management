using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakweneTrackManagement.Application.Common;
using TakweneTrackManagement.Application.Contracts;
using TakweneTrackManagement.Application.DTOs.Artists;
using TakweneTrackManagement.Application.DTOs.TrackDistributions;
using TakweneTrackManagement.Application.DTOs.Tracks;

namespace TakweneTrackManagement.API.Controllers
{

    public class TracksController : ApiBaseController
    {
        private readonly ITrackService _trackService;

        public TracksController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpPost]
        public async Task<ActionResult<TrackDto>> CreateTrack(TrackDto trackDto, CancellationToken ct)
        {
            return ToActionResult(await _trackService.CreateTrackAsync(trackDto, ct));
        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<TrackToReturnDto>>> GetAllTracks(string? Status, int? ArtistId, string? Genre, CancellationToken ct)
        {
            var result = await _trackService.GetAllTracksAsync(Status, ArtistId, Genre, ct);
            return ToActionResult(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TrackToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrackToReturnDto>> GetTrack(int id, CancellationToken ct)
        {
            var result = await _trackService.GetTrackByIdAsync(id, ct);
            return ToActionResult(result);
        }

        [HttpPost("{id}/distribute")]
        public async Task<ActionResult<IReadOnlyList<TrackDistributionDto>>> DistributeTrack(
    int id, DistributeTrackDto dto, CancellationToken ct)
        {
            var result = await _trackService.DistributeTrackAsync(id, dto, ct);
            return ToActionResult(result);
        }
    }
}
