using AutoMapper;
using BlogApp.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BogApp.Entities.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(m => m.Id, i => i.Ignore())
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<PostDto, Post>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
