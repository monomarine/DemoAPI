using AutoMapper;
using DemoAPI.Models;
using DemoAPI.Models.DTO;

namespace DemoAPI.MappingProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostResponseDTO>();
        }
    }
}