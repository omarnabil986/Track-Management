
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakweneTrackManagement.Application.Contracts;
using TakweneTrackManagement.Application.DTOs.Identity;

namespace TakweneTrackManagement.API.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto, CancellationToken cancellationToken)
        => ToActionResult(await _authenticationService.LoginAsync(loginDto, cancellationToken));
    }
}
