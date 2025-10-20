using AutoMapper;
using DemoAPI.Models;
using DemoAPI.Models.DTO;

namespace DemoAPI.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
           
            CreateMap<Tag, TagResponseDTO>();

            
            CreateMap<CreateTagDTO, Tag>();
        }
    }
}