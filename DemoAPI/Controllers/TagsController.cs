using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoAPI.Models;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly APIDBContect _context;

        public TagsController(APIDBContect context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            try
            {
                var tags = await _context.Tags.ToListAsync();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            try
            {
                var tag = await _context.Tags.FindAsync(id);

                if (tag == null)
                {
                    return NotFound($"Tag with ID {id} not found");
                }

                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> CreateTag(Tag tag)
        {
            try
            {
                if (tag == null)
                {
                    return BadRequest("Tag object is null");
                }

                if (string.IsNullOrWhiteSpace(tag.Name))
                {
                    return BadRequest("Name is required");
                }

                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> UpdateTag(int id, Tag tag)
        {
            try
            {
                if (tag == null)
                {
                    return BadRequest("Tag object is null");
                }

                if (id != tag.Id)
                {
                    return BadRequest("ID mismatch");
                }

                if (string.IsNullOrWhiteSpace(tag.Name))
                {
                    return BadRequest("Name is required");
                }

                var existingTag = await _context.Tags.FindAsync(id);
                if (existingTag == null)
                {
                    return NotFound($"Tag with ID {id} not found");
                }

                existingTag.Name = tag.Name;
                existingTag.Description = tag.Description;

                await _context.SaveChangesAsync();

                return Ok(existingTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTag(int id)
        {
            try
            {
                var tag = await _context.Tags.FindAsync(id);
                if (tag == null)
                {
                    return NotFound($"Tag with ID {id} not found");
                }

                _context.Tags.Remove(tag);
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