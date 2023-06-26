using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Account;
using VolunteeringPlatform.Dal.Constants;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.API.Controllers
{
    [AllowAnonymous]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAzureStorageService _azureStorageService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager,
            IAzureStorageService azureStorageService, IAuthenticationService authenticationService,
            ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _azureStorageService = azureStorageService;
            _authenticationService = authenticationService;
            _tokenService = tokenService;
            _mapper = mapper;
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

            User user = _mapper.Map<User>(userForRegisterDto);

            if (userForRegisterDto.Photo != null)
            {
                var imageProps = await _azureStorageService.UploadAsync(userForRegisterDto.Photo, "users", cancellationToken);
                user.ImageName = imageProps.ImageName;
                user.ImageUrl = imageProps.ImageUrl;
            }
            else
            {
                user.ImageUrl = "https://mystorageaccountfortests.blob.core.windows.net/users/default.png";
            }

            var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.User);
                await _signInManager.SignInAsync(user, false);
                return Ok(userForRegisterDto);
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
            
            User user = _mapper.Map<Organization>(organizationForRegisterDto);

            if (organizationForRegisterDto.Image != null)
            {
                var imageProps = await _azureStorageService.UploadAsync(organizationForRegisterDto.Image, "users", cancellationToken);
                user.ImageName = imageProps.ImageName;
                user.ImageUrl = imageProps.ImageUrl;
            }
            else
            {
                user.ImageUrl = "https://mystorageaccountfortests.blob.core.windows.net/projects/default.png";
            }

            var result = await _userManager.CreateAsync(user, organizationForRegisterDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.Organization);
                await _signInManager.SignInAsync(user, false);
                return Ok(organizationForRegisterDto);
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

            User user = _mapper.Map<Volunteer>(volunteerForRegisterDto);

            if (volunteerForRegisterDto.Photo != null)
            {
                var imageProps = await _azureStorageService.UploadAsync(volunteerForRegisterDto.Photo, "users", cancellationToken);
                user.ImageName = imageProps.ImageName;
                user.ImageUrl = imageProps.ImageUrl;
            }
            else
            {
                user.ImageUrl = "https://mystorageaccountfortests.blob.core.windows.net/users/default.png";
            }

            var result = await _userManager.CreateAsync(user, volunteerForRegisterDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.Volunteer);
                await _signInManager.SignInAsync(user, false);
                return Ok(volunteerForRegisterDto);
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
