using DemoAPI.Models.DTO;
using DemoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TagResponseDTO>> GetAll()
        {
            return Ok(_tagService.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<TagResponseDTO> GetById(int id)
        {
            var tag = _tagService.GetById(id);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost]
        public ActionResult<TagResponseDTO> CreateTag([FromBody] CreateTagDTO createTagDTO)
        {
            try
            {
                var tag = _tagService.Create(createTagDTO);
                return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tag);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TagResponseDTO> Update(int id, [FromBody] CreateTagDTO updateTagDTO)
        {
            try
            {
                var updatedTag = _tagService.Update(id, updateTagDTO);
                if (updatedTag == null) return NotFound();
                return Ok(updatedTag);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _tagService.Delete(id);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpGet("{id}/posts")]
        public ActionResult<IEnumerable<PostResponseDTO>> GetPostsByTag(int id)
        {
            var posts = _tagService.GetById(id);
            return Ok(posts);
        }
    }
}
