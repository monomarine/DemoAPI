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
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        private static PostResponseDTO MapToPostDTO(Post post)
        {
            return new PostResponseDTO
            {
                Id = post.Id,
                Title = post.Title,
                CreatedAt = post.CreatedAt,
                Content = post.Content,
                UpdatedAt = post.UpdatedAt,
                Tags = post.Tags.Select(t => new TagResponseDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                })
                .ToList()
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostResponseDTO>> GetPosts()
        {
            var posts = _postRepository.GetAll();
            var postsDTO = posts.Select(MapToPostDTO);
            return Ok(MapToPostDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetPostsById(int id)
        {
            var post = _postRepository.GetById(id);
            if (post == null) return NotFound();

            return Ok(MapToPostDTO(post));
        }

        [HttpPost]
        public ActionResult<PostResponseDTO> Create([FromBody] CreatePostDTO createPostDTO)
        {
            var post = new Post
            {
                Title = createPostDTO.Title,
                Content = createPostDTO.Content,
                CreatedAt = DateTime.UtcNow,
            };

            var createdPost = _postRepository.Create(post);

            return CreatedAtAction(nameof(GetPostsById), new { id = createdPost.Id }, MapToPostDTO(createdPost));
        }

        [HttpPut("{id}")]
        public ActionResult<PostResponseDTO> Update(int id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            var post = _postRepository.GetById(id);

            if (post == null) return NotFound();

            post.Title = updatePostDTO.Title;
            post.Content = updatePostDTO.Content;
            post.UpdatedAt = DateTime.UtcNow;

            var updatedPost = _postRepository.Update(post);

            return Ok(MapToPostDTO(updatedPost));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _postRepository.Delete(id);
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
