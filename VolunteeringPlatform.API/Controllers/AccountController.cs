using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Account;

namespace VolunteeringPlatform.API.Controllers
{
    [AllowAnonymous]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRegistrationService _registrationService;
        private readonly ITokenService _tokenService;

        public AccountController(
            IAuthenticationService authenticationService,
            IRegistrationService registrationService,
            ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _registrationService = registrationService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var passwordSignInResult = await _authenticationService.PasswordSignInAsync(userForLoginDto.Username, userForLoginDto.Password);
            if (passwordSignInResult)
            {
                string accessToken = await _tokenService.GenerateAccessToken(userForLoginDto.Username);
                return Ok(new { AccessToken = accessToken });
            }

            return Unauthorized();
        }

        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterUser([FromForm]UserForRegisterDto userForRegisterDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _registrationService.RegisterUser(userForRegisterDto, cancellationToken);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPost("register/organization")]
        public async Task<IActionResult> RegisterOrganization([FromForm]OrganizationForRegisterDto organizationForRegisterDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _registrationService.RegisterOrganization(organizationForRegisterDto, cancellationToken);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPost("register/volunteer")]
        public async Task<IActionResult> RegisterVolunteer([FromForm]VolunteerForRegisterDto volunteerForRegisterDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _registrationService.RegisterVolunteer(volunteerForRegisterDto, cancellationToken);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
        }
    }
}
