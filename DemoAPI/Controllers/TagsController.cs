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
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TagResponseDTO>> GetAll() =>
            Ok(_service.GetAll().ToList());

        [HttpGet("{id}")]
        public ActionResult<TagResponseDTO> GetById(int id)
        {
            var tag = _service.GetById(id);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost]
        public ActionResult<TagResponseDTO> CreateTag([FromBody] CreateTagDTO createTagDTO)
        {
            try
            {
                var tag = _service.Create(createTagDTO);
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
                var updatedTag = _service.Update(id, updateTagDTO);
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
            var result = _service.Delete(id);
            if (result) return NoContent();
            return NotFound();
        }
    }
}