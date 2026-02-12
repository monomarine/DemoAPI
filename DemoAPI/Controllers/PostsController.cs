using DemoAPI.Models.DTO;
using DemoAPI.Models;
using DemoAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;

        public PostsController(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        private static PostResponseDTO MapToDTO(Post post) => new()
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            Tags = post.Tags.Select(t => new TagResponseDTO { Id = t.Id, Name = t.Name, Description = t.Description }).ToList()
        };

        [HttpGet]
        public ActionResult<IEnumerable<PostResponseDTO>> GetPosts()
        {
            var posts = _postRepository.GetAll();
            return !posts.Any() ? NoContent() : Ok(posts.Select(MapToDTO));
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetPostById(int id)
        {
            if (id <= 0) return BadRequest("Неверный ID поста");
            var post = _postRepository.GetById(id);
            return post == null ? NotFound("Пост не найден") : Ok(MapToDTO(post));
        }

        [HttpPost]
        public ActionResult<PostResponseDTO> Create([FromBody] CreatePostDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Content))
                return BadRequest("Некорректные данные");

            var post = new Post { Title = request.Title, Content = request.Content, CreatedAt = DateTime.UtcNow };

            var createdPost = _postRepository.Create(post);
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, MapToDTO(createdPost));
        }

        [HttpPut("{id}")]
        public ActionResult<PostResponseDTO> Update(int id, [FromBody] CreatePostDTO request)
        {
            if (id <= 0) return BadRequest("Неверный ID поста");
            if (request == null || string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Content))
                return BadRequest("Некорректные данные");

            var post = _postRepository.GetById(id);
            if (post == null) return NotFound("Пост не найден");

            post.Title = request.Title;
            post.Content = request.Content;
            post.UpdatedAt = DateTime.UtcNow;

            var updatedPost = _postRepository.Update(post);
            return Ok(MapToDTO(updatedPost));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest("Неверный ID поста");
            var result = _postRepository.Delete(id);
            return result ? Ok("Пост удален") : NotFound("Пост не найден");
        }
    }
}
