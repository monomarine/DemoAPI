using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;
using DemoAPI.Models.DTO;
using DemoAPI.Services;

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
        public ActionResult<TagResponseDTO> GetTag(int id)
        {
            var tag = _tagService.GetById(id);

            if (tag == null)
                return NotFound($"еег с ID {id} не найден");

            return Ok(tag);
        }

        [HttpPost]
        public ActionResult<TagResponseDTO> CreateTag([FromBody] CreateTagDTO createTagDTO)
        {
            if (createTagDTO == null)
                return BadRequest("данные тега не могут быть пустыми");

            if (string.IsNullOrEmpty(createTagDTO.Name))
                return BadRequest("нзвание тега должно быть заполнено");

            try
            {
                var createdTag = _tagService.Create(createTagDTO);
                return CreatedAtAction(nameof(GetTag), new { id = createdTag.Id }, createdTag);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"ошибка создания тега: {ex.Message}");
            }
        }

        [HttpPut]
        public ActionResult<TagResponseDTO> UpdateTag(int id, [FromBody] CreateTagDTO updateTagDTO)
        {
            if (updateTagDTO == null)
                return BadRequest("данные для обновления не могут быть пустыми");

            if (string.IsNullOrEmpty(updateTagDTO.Name))
                return BadRequest("название тега должно быть запонено");

            try
            {
                var updatedTag = _tagService.Update(id, updateTagDTO);

                if (updatedTag == null)
                    return NotFound($"тег с ID {id} не найден");

                return Ok(updatedTag);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"ошибка обновления тега: {ex.Message}");
            }
        }

        [HttpDelete]
        public ActionResult DeleteTag(int id)
        {
            var result = _tagService.Delete(id);

            if (result)
                return NoContent();
            else
                return NotFound($"тег с ID {id} не найден");
        }
    }
}
