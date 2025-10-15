using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Repositories;
using DemoAPI.Models;
using DemoAPI.Models.DTO;
using System.Reflection.Metadata.Ecma335;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        private static TagResponseDTO MapToTagDTO(Tag tag)
        {
            return new TagResponseDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<TagResponseDTO>> GetTags()
        {
            var tags = _tagRepository.GetAll();
            var tagsDTO = tags.Select(MapToTagDTO);
            return Ok(MapToTagDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<TagResponseDTO> GetTagsById(int id)
        {
            var tag = _tagRepository.GetById(id);
            if (tag == null) return NotFound();

            return Ok(MapToTagDTO(tag));
        }

        [HttpPost]
        public ActionResult<TagResponseDTO> Create([FromBody] CreateTagDTO createTagDTO)
        {
            var tag = new Tag
            {
                Name = createTagDTO.Name,
                Description = createTagDTO.Description,
            };

            var createdTag = _tagRepository.Create(tag);

            return CreatedAtAction(nameof(GetTagsById), new { id = createdTag.Id }, MapToTagDTO(createdTag));
        }

        [HttpPut("{id}")]
        public ActionResult<TagResponseDTO> Update(int id, [FromBody] CreateTagDTO updateTagDTO)
        {
            var tag = _tagRepository.GetById(id);

            if (tag == null) return NotFound();

            tag.Name = updateTagDTO.Name;
            tag.Description = updateTagDTO.Description;

            var updatedTag = _tagRepository.Update(tag);

            return Ok(MapToTagDTO(updatedTag));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _tagRepository.Delete(id);
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
