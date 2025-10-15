using DemoAPI.Models.DTO;
using DemoAPI.Repositories;
using DemoAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController (ITagService tagServ)
        {
            _tagService = tagServ;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TagResponseDTO>> GetAll() => Ok(_tagService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<TagResponseDTO> GetById(int id)
        {
            try
            {
                return Ok(_tagService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpPost]
        public ActionResult<TagResponseDTO> CreateTag([FromBody] CreateTagDTO create)
        {
            try
            {
                var tag = _tagService.Create(create);
                return Ok(tag);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TagResponseDTO> Update(int id, [FromBody] CreateTagDTO updateTag)
        {
            try
            {
                return Ok(_tagService.Update(id, updateTag));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _tagService.Delete(id);
            return NoContent();
        }
    }
}
