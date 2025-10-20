using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Services;
using DemoAPI.Models;
using DemoAPI.Models.DTO;
using AutoMapper;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagsController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TagResponseDTO>> GetAllTags()
        {
            var tags = _tagService.GetAll();
            var tagDTO = _mapper.Map<IEnumerable<TagResponseDTO>>(tags); 
            return Ok(tagDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<TagResponseDTO> GetTagById(int id)
        {
            var tag = _tagService.GetById(id);

            if (tag == null)
                return NotFound();

            return Ok(_mapper.Map<TagResponseDTO>(tag));
        }

        [HttpPost]
        public ActionResult<TagResponseDTO> CreateTag([FromBody] CreateTagDTO createTagDTO)
        {
            try
            {
                var createdTag = _tagService.Create(createTagDTO);
                return CreatedAtAction(nameof(GetTagById), new { id = createdTag.Id }, _mapper.Map<TagResponseDTO>(createdTag));
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

                return Ok(_mapper.Map<TagResponseDTO>(updatedTag));
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