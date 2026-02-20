using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoAPI.Models;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly APIDBContect _context;

        public PostsController(APIDBContect context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            try
            {
                var posts = await _context.Posts.ToListAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);

                if (post == null)
                {
                    return NotFound($"Post with ID {id} not found");
                }

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            try
            {
                if (post == null)
                {
                    return BadRequest("Post object is null");
                }

                if (string.IsNullOrWhiteSpace(post.Title))
                {
                    return BadRequest("Title is required");
                }

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost(int id, Post post)
        {
            try
            {
                if (post == null)
                {
                    return BadRequest("Post object is null");
                }

                if (id != post.Id)
                {
                    return BadRequest("ID mismatch");
                }

                if (string.IsNullOrWhiteSpace(post.Title))
                {
                    return BadRequest("Title is required");
                }

                var existingPost = await _context.Posts.FindAsync(id);
                if (existingPost == null)
                {
                    return NotFound($"Post with ID {id} not found");
                }

                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                existingPost.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(existingPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null)
                {
                    return NotFound($"Post with ID {id} not found");
                }

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}