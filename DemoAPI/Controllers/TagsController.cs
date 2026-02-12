using DemoAPI.Models.DTO;
using DemoAPI.Models;
using DemoAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        public TagsController(ITagRepository tagRepository) => _tagRepository = tagRepository;

        private static TagResponseDTO MapToDTO(Tag tag) => new()
        {
            Id = tag.Id,
            Name = tag.Name,
            Description = tag.Description
        };

        [HttpGet]
        public ActionResult<IEnumerable<TagResponseDTO>> GetTags()
        {
            var tags = _tagRepository.GetAll();
            return !tags.Any() ? NoContent() : Ok(tags.Select(MapToDTO));
        }

        [HttpGet("{id}")]
        public ActionResult<TagResponseDTO> GetTagById(int id)
        {
            if (id <= 0) return BadRequest("Неверный ID тега");
            var tag = _tagRepository.GetById(id);
            return tag == null ? NotFound("Тег не найден") : Ok(MapToDTO(tag));
        }

        [HttpPost]
        public ActionResult<TagResponseDTO> Create([FromBody] CreateTagDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Название тега обязательно");

            var tag = new Tag { Name = request.Name, Description = request.Description };
            var createdTag = _tagRepository.Create(tag);
            return CreatedAtAction(nameof(GetTagById), new { id = createdTag.Id }, MapToDTO(createdTag));
        }

        [HttpPut("{id}")]
        public ActionResult<TagResponseDTO> Update(int id, [FromBody] CreateTagDTO request)
        {
            if (id <= 0) return BadRequest("Неверный ID тега");
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Название тега обязательно");

            var tag = _tagRepository.GetById(id);
            if (tag == null) return NotFound("Тег не найден");

            tag.Name = request.Name;
            tag.Description = request.Description;

            return Ok(MapToDTO(_tagRepository.Update(tag)));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest("Неверный ID тега");
            var result = _tagRepository.Delete(id);
            return result ? Ok("Тег удален") : NotFound("Тег не найден");
        }
    }
}
