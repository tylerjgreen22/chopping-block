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
            CreateMap<Post, PostListToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Likes, o => o.MapFrom(s => s.Likes.Count))
                .ForMember(d => d.User, o => o.MapFrom(s => s.User.UserName));
            CreateMap<Post, PostToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Likes, o => o.MapFrom(s => s.Likes.Count))
                .ForMember(d => d.User, o => o.MapFrom(s => s.User.UserName));
            CreateMap<PostDto, Post>();

            CreateMap<Step, StepToReturnDto>();
        }
    }
}