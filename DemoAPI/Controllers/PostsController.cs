using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;

namespace DemoAPI_11.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private static List<Post> _posts = new List<Post>
        {
            new Post { Id = 1, Title = "First Post", Content = "Content 1" }
        };

        // GET: api/posts
        [HttpGet]
        public IActionResult Get()
        {
            if (_posts.Count == 0)
                return NoContent();

            return Ok(_posts);
        }

        // GET api/posts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return NoContent();

            return Ok(post);
        }

        // POST api/posts
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

        // PUT api/posts/5
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

        // DELETE api/posts/5
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