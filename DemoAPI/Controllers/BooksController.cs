using DemoAPI.Models.DTO;
using DemoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper; 
using System.Runtime.CompilerServices;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper; 

        public BooksController(IBookService service, IMapper mapper) 
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetAll()
        {
            var books = _service.GetAllbooks();
            var bookDtos = _mapper.Map<IEnumerable<BookDTO>>(books); 
            return Ok(bookDtos.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<BookDTO> GetById(int id)
        {
            var book = _service.GetById(id);
            if (book == null) return NotFound();

            var bookDto = _mapper.Map<BookDTO>(book); 
            return Ok(bookDto);
        }

        [HttpPost]
        public ActionResult<BookDTO> CreateBook([FromBody] CreateBookDTO createBookDTO)
        {
            try
            {
                var book = _service.Create(createBookDTO);
                var bookDto = _mapper.Map<BookDTO>(book); 
                return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<BookDTO> Update(int id, [FromBody] UpdateBookDTO updateBookDTO)
        {
            try
            {
                var updateBook = _service.Update(id, updateBookDTO);
                if (updateBook == null) return NotFound();

                var bookDto = _mapper.Map<BookDTO>(updateBook); 
                return Ok(bookDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result) return NoContent();
            return NotFound();
        }
    }
}