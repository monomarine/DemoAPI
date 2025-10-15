using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models.DTO;
using DemoAPI.Services;

namespace DemoAPI.Controllers
{
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
        public ActionResult<IEnumerable<PostResponseDTO>> GetAllPosts()
        {
            var posts = _postService.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetPostById(int id)
        {
            var post = _postService.GetById(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public ActionResult<PostResponseDTO> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            try
            {
                var createdPost = _postService.Create(createPostDTO);
                return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PostResponseDTO> UpdatePost(int id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            try
            {
                var updatedPost = _postService.Update(id, updatePostDTO);
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
        public ActionResult DeletePost(int id)
        {
            var result = _postService.Delete(id);
            if (result)
                return NoContent();

            return NotFound();
        }
    }
}