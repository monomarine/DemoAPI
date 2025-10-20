using AutoMapper;
using DemoAPI.Models;
using DemoAPI.Models.DTO;
using DemoAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public AuthorsController (IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        private static AuthorDTO MapAuthorDTO(Author author)
        {
            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                Alias = author.Alias,
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthors()
        {
            var authors = _authorRepository.GetAll();
            var authorsDTOs = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
            return Ok(authorsDTOs);
        }

        [HttpGet("{id}")]
        public ActionResult<AuthorDTO> GetAuthorById(int id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null) return NotFound();

            return Ok(MapAuthorDTO(author));
        }

        [HttpPost]
        public ActionResult<AuthorDTO> Create([FromBody] CreateAuthorDTO createAuthorDTO)
        {
            var author = new Author
            {
                Name = createAuthorDTO.Name,
                Alias = createAuthorDTO.Alias,
            };

            var createdAuthor = _authorRepository.Create(author);

            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, MapAuthorDTO(createdAuthor));
        }

        [HttpPut("{id}")]
        public ActionResult<AuthorDTO> Update(int id, [FromBody] CreateAuthorDTO updateAuthorDTO)
        {
            var author = _authorRepository.GetById(id);

            if (author == null) return NotFound();

            author.Alias = updateAuthorDTO.Alias;
            author.Name = updateAuthorDTO.Name;

            var updatedAuthor = _authorRepository.Update(author);

            return Ok(MapAuthorDTO(updatedAuthor));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _authorRepository.Delete(id);
            if (result)
                return Ok();
            return NotFound();
        }
        
        

    }
}
