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
            CreateMap<RecipePost, PostToReturnDto>()
                .ForMember(d => d.RecipeCategory, o => o.MapFrom(s => s.RecipeCategory.Category))
                .ForMember(d => d.Picture, o => o.MapFrom<PostUrlResolver>());

            CreateMap<RecipeStep, StepToReturnDto>();
        }
    }
}