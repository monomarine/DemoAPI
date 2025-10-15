using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;
using DemoAPI.Models.DTO;
using DemoAPI.Services;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostResponseDTO>> GetAllPosts()
        {
            var posts = _postService.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetPost(int id)
        {
            var post = _postService.GetById(id);

            if (post == null)
                return NotFound($"gост с ID {id} не найден");

            return Ok(post);
        }

        [HttpPost]
        public ActionResult<PostResponseDTO> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            if (createPostDTO == null)
                return BadRequest("днные поста не могут быть пустыми");

            if (string.IsNullOrEmpty(createPostDTO.Title) ||
                string.IsNullOrEmpty(createPostDTO.Content))
                return BadRequest("заголовок и содержание поста должны быть заполнены");

            try
            {
                var createdPost = _postService.Create(createPostDTO);
                return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
            }
            catch (Exception ex)
            {
                return BadRequest($"ошибка создания поста: {ex.Message}");
            }
        }

        [HttpPut]
        public ActionResult<PostResponseDTO> UpdatePost(int id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            if (updatePostDTO == null)
                return BadRequest("данные для обновления не могут быть пустыми");


            try
            {
                var updatedPost = _postService.Update(id, updatePostDTO);
                return Ok(updatedPost);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"ошибка обновления поста: {ex.Message}");
            }
        }

        [HttpDelete]
        public ActionResult DeletePost(int id)
        {
            var result = _postService.Delete(id);

            if (result)
                return NoContent();
            else
                return NotFound($"пост с ID {id} не найден");
        }
    }
}