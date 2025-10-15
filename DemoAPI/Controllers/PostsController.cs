using DemoAPI.Models.DTO;
using DemoAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postServ;

        public PostsController(IPostService postService)
        {
            _postServ = postService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostResponseDTO>> GetAll() => Ok(_postServ.GetAllPosts());

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetById(int id)
        {
            try
            {
                return Ok(_postServ.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public ActionResult<PostResponseDTO> CreateTag([FromBody] CreatePostDTO create)
        {
            try
            {
                var post = _postServ.Create(create);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PostResponseDTO> Update(int id, [FromBody] UpdatePostDTO updatePost)
        {
            try
            {
                return Ok(_postServ.Update(id, updatePost));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _postServ.Delete(id);
            return NoContent();
        }
    }
}
