using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _repository;
        public AuthorController(IAuthorRepository repo)
        {
            _repository = repo;
        }
        [HttpGet]
        public ActionResult<Author> GetAllAuthors()
        {
            return Ok(_repository.GetAllAuthors());
        }
    }
}
