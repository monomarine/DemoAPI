using DemoAPI.Models.DTO;
using DemoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<PostResponseDTO>> GetAll() =>
            Ok(_service.GetAllPosts().ToList());

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetById(int id)
        {
            var post = _service.GetById(id);
            if (post == null) return NotFound();
            return Ok(post);
        }
        [HttpPost]
        public ActionResult<PostResponseDTO> CreatePosts([FromBody] CreatePostDTO createPostDTO)
        {
            try
            {
                var post = _service.Create(createPostDTO);
                return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
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
                var updatePost = _service.Update(id, updatePostDTO);
                if (updatePost == null) return NotFound();
                return Ok(updatePost);
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
