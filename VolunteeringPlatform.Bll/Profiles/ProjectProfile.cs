using AutoMapper;
using VolunteeringPlatform.Common.Dtos.Project;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Bll.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectListDto>()
                .ForMember(x => x.Organization, y => y.MapFrom(s => s.Organization.FullName));

            CreateMap<Project, ProjectDto>()
                .ForMember(x => x.Organization, y => y.MapFrom(s => s.Organization.FullName));

            CreateMap<ProjectForCreateDto, Project>();
            CreateMap<ProjectForUpdateDto, Project>();
            CreateMap<Project, MyProjectsListDto>();

            CreateMap<ProjectVolunteer, ProjectVolunteerListDto>()
                .ForMember(x => x.FullName, y => y.MapFrom(s => s.Volunteer.FullName))
                .ForMember(x => x.ImageUrl, y => y.MapFrom(s => s.Volunteer.ImageUrl));

            CreateMap<ProjectVolunteer, ParticipationsListDto>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Project.Name))
                .ForMember(x => x.ImageUrl, y => y.MapFrom(s => s.Project.ImageUrl));
        }
    }
}
