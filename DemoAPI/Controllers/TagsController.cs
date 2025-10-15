using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Services;
using DemoAPI.Models;
using DemoAPI.Models.DTO;

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
        public ActionResult<IEnumerable<TagResponseDTO>> GetAllTags()
        {
            var tags = _tagService.GetAll();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public ActionResult<TagResponseDTO> GetTagById(int id)
        {
            var tag = _tagService.GetById(id);

            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPost]
        public ActionResult<TagResponseDTO> CreateTag([FromBody] CreateTagDTO createTagDTO)
        {
            try
            {
                var createdTag = _tagService.Create(createTagDTO);
                return CreatedAtAction(nameof(GetTagById), new { id = createdTag.Id }, createdTag);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TagResponseDTO> UpdateTag(int id, [FromBody] CreateTagDTO updateTagDTO)
        {
            try
            {
                var updatedTag = _tagService.Update(id, updateTagDTO);

                if (updatedTag == null)
                    return NotFound();

                return Ok(updatedTag);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTag(int id)
        {
            var result = _tagService.Delete(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}