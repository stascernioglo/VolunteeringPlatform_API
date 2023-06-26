using AutoMapper;
using VolunteeringPlatform.Common.Dtos.Account;
using VolunteeringPlatform.Common.Dtos.Volunteer;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Bll.Profiles
{
    public class VolunteerProfile : Profile
    {
        public VolunteerProfile()
        {
            CreateMap<VolunteerForRegisterDto, Volunteer>()
                .ForMember(x => x.Description, y => y.MapFrom(s => s.PersonalInformation))
                .ForMember(x => x.PasswordHash, y => y.MapFrom(s => s.Password));

            CreateMap<Volunteer, VolunteerListDto>();
        }
    }
}
