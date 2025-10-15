using DemoAPI.Models.DTO;
using DemoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    // PostController
    [Route("api/[controller]")]
    [ApiController]
    
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostResponseDTO>> GetAll()
        {
            return Ok(_postService.GetAllPosts().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetById(int id)
        {
            var post = _postService.GetById(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public ActionResult<PostResponseDTO> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            try
            {
                var post = _postService.Create(createPostDTO);
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
                var updatedPost = _postService.Update(id, updatePostDTO);
                if (updatedPost == null) return NotFound();
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
            var result = _postService.Delete(id);
            if (result) return NoContent();
            return NotFound();
        }

    }
}