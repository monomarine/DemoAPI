using AutoMapper;
using DemoAPI.Models;
using DemoAPI.Models.DTO;

namespace DemoAPI.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
           
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

            
            CreateMap<CreateBookDTO, Book>();

            
            CreateMap<UpdateBookDTO, Book>();
        }
    }
}