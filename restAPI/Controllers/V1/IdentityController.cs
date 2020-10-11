using Microsoft.AspNetCore.Mvc;
using restAPI.Contracts.V1;
using restAPI.Contracts.V1.Reqests;
using restAPI.Contracts.V1.Responses;
using restAPI.Services;
using System.Linq;
using System.Threading.Tasks;

namespace restAPI.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /*
         * POST
         * api/v1/identity/register
         * Register new user
         */
        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            // Validated the email field
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return Unauthorized(new AuthFailResponse { Errors = authResponse.Errors });
            }

            return Ok(new AuthSuccessResponse { 
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /*
         * POST
         * api/v1/identity/login
         * Login the user
         */
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {

            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return Unauthorized(new AuthFailResponse { Errors = authResponse.Errors });
            }

            return Ok(new AuthSuccessResponse { 
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /*
         * POST
         * api/v1/identity/refresh
         * Gets new refresh token
         */
        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Login([FromBody] RefreshTokenRequest request)
        {

            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return Unauthorized(new AuthFailResponse { Errors = authResponse.Errors });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}
