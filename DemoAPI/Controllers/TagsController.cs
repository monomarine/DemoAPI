using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;

namespace DemoAPI_11.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private static List<Tag> _tags = new List<Tag>
        {
            new Tag { Id = 1, Name = "Technology" }
        };

        // GET: api/tags
        [HttpGet]
        public IActionResult Get()
        {
            if (_tags.Count == 0)
                return NoContent();

            return Ok(_tags);
        }

        // GET api/tags/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tag = _tags.FirstOrDefault(t => t.Id == id);
            if (tag == null)
                return NoContent();

            return Ok(tag);
        }

        // POST api/tags
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

        // PUT api/tags/5
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

        // DELETE api/tags/5
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
