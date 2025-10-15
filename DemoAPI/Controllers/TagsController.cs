using DemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private static List<Tag> _tags = new List<Tag>
        {
            new Tag { Id = 1, Name = "Technology" }
        };

        [HttpGet]
        public IActionResult Get()
        {
            if (_tags.Count == 0)
                return NoContent();

            return Ok(_tags);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tag = _tags.FirstOrDefault(t => t.Id == id);
            if (tag == null)
                return NoContent();

            return Ok(tag);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Tag tag)
        {
            if (tag == null || !ModelState.IsValid)
                return BadRequest("Invalid data");

            if (_tags.Any(t => t.Id == tag.Id))
                return BadRequest("Duplicate ID");

            _tags.Add(tag);
            return Ok(tag);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tag updatedTag)
        {
            if (updatedTag == null || id != updatedTag.Id || !ModelState.IsValid)
                return BadRequest("Invalid data");

            var existingTag = _tags.FirstOrDefault(t => t.Id == id);
            if (existingTag == null)
                return NoContent();

            existingTag.Name = updatedTag.Name;
            return Ok(existingTag);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tag = _tags.FirstOrDefault(t => t.Id == id);
            if (tag == null)
                return NoContent();

            _tags.Remove(tag);
            return Ok("Tag deleted");
        }
    }
}
