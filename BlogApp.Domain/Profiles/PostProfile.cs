using AutoMapper;
using BlogApp.Contracts;

namespace BogApp.Domain.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostRequest, Post>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Post, GetPostResponse>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
