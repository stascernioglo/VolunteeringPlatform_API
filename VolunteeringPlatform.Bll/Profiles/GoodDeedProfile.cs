using AutoMapper;
using VolunteeringPlatform.Common.Dtos.GoodDeed;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Bll.Profiles
{
    public class GoodDeedProfile : Profile
    {
        public GoodDeedProfile()
        {
            CreateMap<GoodDeed, GoodDeedListDto>();

            CreateMap<GoodDeed, GoodDeedDto>()
                .ForMember(x => x.User, y => y.MapFrom(s => s.User.FullName));

            CreateMap<GoodDeedForCreateDto, GoodDeed>();
            CreateMap<GoodDeedForUpdateDto, GoodDeed>();
        }
    }
}
