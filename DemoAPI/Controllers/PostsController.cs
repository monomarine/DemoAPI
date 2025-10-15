using DemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private static List<Post> _posts = new List<Post>
        {
            new Post { Id = 1, Title = "First Post", Content = "Content 1" }
        };
        [HttpGet]
        public IActionResult Get()
        {
            if (_posts.Count == 0)
                return NoContent();

            return Ok(_posts);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return NoContent();

            return Ok(post);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            if (post == null || !ModelState.IsValid)
                return BadRequest("Invalid data");

            if (_posts.Any(p => p.Id == post.Id))
                return BadRequest("Duplicate ID");

            _posts.Add(post);
            return Ok(post);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Post updatedPost)
        {
            if (updatedPost == null || id != updatedPost.Id || !ModelState.IsValid)
                return BadRequest("Invalid data");

            var existingPost = _posts.FirstOrDefault(p => p.Id == id);
            if (existingPost == null)
                return NoContent();

            existingPost.Title = updatedPost.Title;
            existingPost.Content = updatedPost.Content;
            return Ok(existingPost);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return NoContent();

            _posts.Remove(post);
            return Ok("Post deleted");
        }
    }
}
