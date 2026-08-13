using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakweneTrackManagement.Application.Common;
using TakweneTrackManagement.Application.Contracts;
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

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<TrackDto>>> GetAllTracks(CancellationToken ct)
        {
            var result = await _trackService.GetAllTracksAsync(ct);
            return ToActionResult(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TrackDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrackDto>> GetTrack(int id, CancellationToken ct)
        {
            var result = await _trackService.GetTrackByIdAsync(id, ct);
            return ToActionResult(result);
        }
    }
}
