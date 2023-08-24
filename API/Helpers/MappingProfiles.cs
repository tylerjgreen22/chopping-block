using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    // Mapping profiles for auto mapper to map return entities to DTOs. Also augments image URL to include the url specified in the appsettings using PostUrlResolver
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Post, PostToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Picture, o => o.MapFrom<PostUrlResolver>());

            CreateMap<Step, StepToReturnDto>();
        }
    }
}