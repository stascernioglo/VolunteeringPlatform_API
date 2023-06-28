using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Common.Dtos.Account;
using VolunteeringPlatform.Dal.Constants;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Bll.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _azureStorageService;

        public RegistrationService(
            SignInManager<User> signInManager, 
            UserManager<User> userManager, 
            IMapper mapper, 
            IAzureStorageService azureStorageService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _azureStorageService = azureStorageService;
        }

        public async Task<IdentityResult> RegisterOrganization(OrganizationForRegisterDto organizationForRegisterDto, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<Organization>(organizationForRegisterDto);

            if (organizationForRegisterDto.Image != null)
            {
                var imageProps = await _azureStorageService.UploadAsync(organizationForRegisterDto.Image, "users", cancellationToken);
                user.ImageName = imageProps.ImageName;
                user.ImageUrl = imageProps.ImageUrl;
            }
            else
            {
                user.ImageUrl = "https://utmstorageaccount.blob.core.windows.net/projects/default.png";
            }

            var result = await _userManager.CreateAsync(user, organizationForRegisterDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.Organization);
                await _signInManager.SignInAsync(user, false);         
            }
            return result;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegisterDto userForRegisterDto, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(userForRegisterDto);

            if (userForRegisterDto.Photo != null)
            {
                var imageProps = await _azureStorageService.UploadAsync(userForRegisterDto.Photo, "users", cancellationToken);
                user.ImageName = imageProps.ImageName;
                user.ImageUrl = imageProps.ImageUrl;
            }
            else
            {
                user.ImageUrl = "https://utmstorageaccount.blob.core.windows.net/users/default.png";
            }

            var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.User);
                await _signInManager.SignInAsync(user, false);
            }
            return result;
        }

        public async Task<IdentityResult> RegisterVolunteer(VolunteerForRegisterDto volunteerForRegisterDto, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<Volunteer>(volunteerForRegisterDto);

            if (volunteerForRegisterDto.Photo != null)
            {
                var imageProps = await _azureStorageService.UploadAsync(volunteerForRegisterDto.Photo, "users", cancellationToken);
                user.ImageName = imageProps.ImageName;
                user.ImageUrl = imageProps.ImageUrl;
            }
            else
            {
                user.ImageUrl = "https://utmstorageaccount.blob.core.windows.net/users/default.png";
            }

            var result = await _userManager.CreateAsync(user, volunteerForRegisterDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.Volunteer);
                await _signInManager.SignInAsync(user, false);
            }

            return result;
        }
    }
}
