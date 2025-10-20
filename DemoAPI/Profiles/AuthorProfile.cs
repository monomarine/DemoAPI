using AutoMapper;
using DemoAPI.Models;
using DemoAPI.Models.DTO;

namespace DemoAPI.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            
            CreateMap<Author, AuthorDTO>();

            
            CreateMap<CreateAuthorDTO, Author>();
        }
    }
}