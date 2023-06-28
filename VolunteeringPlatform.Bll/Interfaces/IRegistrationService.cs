using Microsoft.AspNetCore.Identity;
using VolunteeringPlatform.Common.Dtos.Account;

namespace VolunteeringPlatform.Bll.Interfaces
{
    public interface IRegistrationService
    {
        Task<IdentityResult> RegisterUser(UserForRegisterDto userForRegisterDto, CancellationToken cancellationToken);

        Task<IdentityResult> RegisterOrganization(OrganizationForRegisterDto organizationForRegisterDto, CancellationToken cancellationToken);

        Task<IdentityResult> RegisterVolunteer(VolunteerForRegisterDto volunteerForRegisterDto, CancellationToken cancellationToken);
    }
}
