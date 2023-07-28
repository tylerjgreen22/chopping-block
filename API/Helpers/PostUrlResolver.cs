using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    // Resolver that retrieves api url from appsettings and resolves the picture url to the apiurl followed the by the picture url
    public class PostUrlResolver : IValueResolver<RecipePost, PostToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public PostUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(RecipePost source, PostToReturnDto dest, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Picture))
            {
                return _config["ApiUrl"] + source.Picture + ".jpg";
            }

            return null;
        }
    }
}