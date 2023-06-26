using AutoMapper;
using VolunteeringPlatform.Common.Dtos.Account;
using VolunteeringPlatform.Common.Dtos.Organization;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Bll.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<OrganizationForRegisterDto, Organization>()
                .ForMember(x => x.PasswordHash, y => y.MapFrom(s => s.Password));

            CreateMap<Organization, OrganizationDto>();
            CreateMap<Organization, OrganizationListDto>();
        }
    }
}
