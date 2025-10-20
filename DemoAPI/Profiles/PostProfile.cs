using AutoMapper;
using DemoAPI.Models;
using DemoAPI.Models.DTO;

namespace DemoAPI.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
           
            CreateMap<Post, PostResponseDTO>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            
            CreateMap<CreatePostDTO, Post>()
                .ForMember(dest => dest.Tags, opt => opt.Ignore()); 

            
            CreateMap<UpdatePostDTO, Post>()
                .ForMember(dest => dest.Tags, opt => opt.Ignore()); 
        }
    }
}