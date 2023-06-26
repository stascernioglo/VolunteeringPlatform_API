using AutoMapper;
using VolunteeringPlatform.Common.Dtos.Account;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Bll.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForRegisterDto, User>()
                .ForMember(x => x.Description, y => y.MapFrom(s => s.PersonalInformation))
                .ForMember(x => x.PasswordHash, y => y.MapFrom(s => s.Password));
        }
    }
}
