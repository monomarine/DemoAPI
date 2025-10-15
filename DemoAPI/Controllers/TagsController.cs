using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Services;
using DemoAPI.Models.DTO;

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
        public ActionResult<IEnumerable<TagResponseDTO>> GetAll()
        {
            var tags = _service.GetAll();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public ActionResult<TagResponseDTO> GetById(int id)
        {
            var tag = _service.GetById(id);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPost]
        public ActionResult<TagResponseDTO> Create([FromBody] CreateTagDTO createTagDTO)
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
        public ActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result)
                return NoContent();

            return NotFound();
        }
    }
}