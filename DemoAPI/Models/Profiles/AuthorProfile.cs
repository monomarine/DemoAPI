using AutoMapper;
using DemoAPI.Models.DTO;

namespace DemoAPI.Models.Profiles
{
    public class AuthorProfile: Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author,AuthorDTO>();
        }
    }
}
