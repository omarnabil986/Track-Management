using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakweneTrackManagement.Application.Contracts;
using TakweneTrackManagement.Application.DTOs.Artists;
using TakweneTrackManagement.Application.DTOs.Tracks;

namespace TakweneTrackManagement.API.Controllers
{
 
    public class ArtistsController : ApiBaseController
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpPost]
        public async Task<ActionResult<ArtistDto>> CreateArtist(ArtistDto artistDto, CancellationToken ct)
        {
            return ToActionResult(await _artistService.CreateArtistAsync(artistDto, ct));
        }


        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<ArtistDto>>> GetAllArtists(CancellationToken ct)
        {
            var result = await _artistService.GetAllArtistsAsync(ct);
            return ToActionResult(result);
        }

    }
}
