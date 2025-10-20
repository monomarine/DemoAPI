using AutoMapper;
using DemoAPI.Models.DTO;
using DemoAPI.Models;

namespace DemoAPI.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile() 
        {
            CreateMap<Author, AuthorDTO>();
        }
    }
}
