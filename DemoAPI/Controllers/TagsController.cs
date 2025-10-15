using DemoAPI.Models.DTO;
using DemoAPI.Services;
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
        public ActionResult<IEnumerable<TagResponseDTO>> GetAllTags() => Ok(_tagService.GetAll().ToList());

        [HttpGet("{id}")]
        public ActionResult<TagResponseDTO> GetTagById(int id)
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
                return CreatedAtAction(nameof(GetTagById), new { id = tag.Id }, tag);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TagResponseDTO> Update(int id, [FromBody] CreateTagDTO updateTagDTO)
        {
            try
            {
                var updateTag = _tagService.Update(id, updateTagDTO);
                if (updateTag == null) return NotFound();
                
                return Ok(updateTagDTO);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
        }
        
        [HttpDelete("{id}")]
        public ActionResult<TagResponseDTO> DeleteTag(int id) 
        {
            var result = _tagService.Delete(id);
            if (result) return NoContent();
            
            return NotFound();
        }
    }
}