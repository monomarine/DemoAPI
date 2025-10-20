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
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostResponseDTO>> GetAllPosts()
        {
            var posts = _postService.GetAllPosts();
            var postDTO = _mapper.Map<IEnumerable<PostResponseDTO>>(posts); 
            return Ok(postDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetPostById(int id)
        {
            var post = _postService.GetById(id);

            if (post == null)
                return NotFound();

            return Ok(_mapper.Map<PostResponseDTO>(post));
        }

        [HttpPost]
        public ActionResult<PostResponseDTO> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            try
            {
                var createdPost = _postService.Create(createPostDTO);
                return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, _mapper.Map<PostResponseDTO>(createdPost));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PostResponseDTO> UpdatePost(int id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            try
            {
                var updatedPost = _postService.Update(id, updatePostDTO);

                if (updatedPost == null)
                    return NotFound();

                return Ok(_mapper.Map<PostResponseDTO>(updatedPost));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePost(int id)
        {
            var result = _postService.Delete(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}