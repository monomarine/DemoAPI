using AutoMapper;
using DemoAPI.Models.DTO;
using DemoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<BookDTO>> GetAll() =>
            Ok(_service.GetAllbooks().ToList());


        [HttpGet("{id}")]
        public ActionResult<BookDTO> GetById(int id)
        {
            var book = _service.GetById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpPost]
        public ActionResult<BookDTO> CreateBook([FromBody] CreateBookDTO createBookDTO)
        {
            try
            {
                var book = _service.Create(createBookDTO);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<BookDTO> Update(int id,[FromBody] UpdateBookDTO updateBookDTO)
        {
            try
            {
                var updateBook = _service.Update(id, updateBookDTO);
                if (updateBook == null) return NotFound();
                return Ok(updateBook);
            }
            catch(ArgumentException ex)
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
