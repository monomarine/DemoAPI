using DemoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;
        public BookController(IBookRepository repo)
        {
            _repository = repo;
        }
        [HttpPost]
        public ActionResult<Book> CreateBook([FromBody] Book book)
        {
            try
            {
                var newBook = _repository.CreateNewBook(book);
                return Ok(newBook);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest("некорретный формат книги");
            }
        }

    }
}
