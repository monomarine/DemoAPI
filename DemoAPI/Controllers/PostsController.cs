using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Services;
using DemoAPI.Models.DTO;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;

        public PostsController(IPostService service)
        {
            _service = service;

        }

        [HttpGet]
        public ActionResult<IEnumerable<PostResponseDTO>> GetAll()
        {
            var posts = _service.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetById(int id)
        {
            var post = _service.GetById(id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public ActionResult<PostResponseDTO> Create([FromBody] CreatePostDTO createPostDTO)
        {
            try
            {
                var post = _service.Create(createPostDTO);
                return CreatedAtAction(nameof(GetById), new {id = post.Id}, post);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PostResponseDTO> Update(int id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            try
            {
                var updatedPost = _service.Update(id, updatePostDTO);
                if (updatedPost == null)
                    return NotFound();

                return Ok(updatedPost);
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
            if (result)
                return NoContent();

            return NotFound();
        }
    }
}