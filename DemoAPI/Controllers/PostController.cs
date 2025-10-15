using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Repositories;
using DemoAPI.Models;
using DemoAPI.Models.DTO;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        private static PostResponseDTO MapPostDTO(Post post)
        {
            return new PostResponseDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
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
        public ActionResult<IEnumerable<PostResponseDTO>> GetAuthors()
        {
            var authors = _postRepository.GetAll();
            var authorsDTO = authors.Select(MapPostDTO);
            return Ok(authorsDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetPostById(int id)
        {
            var post = _postRepository.GetById(id);
            if (post == null) return NotFound();

            return Ok(MapPostDTO(post));
        }

        [HttpPost]
        public ActionResult<PostResponseDTO> Create([FromBody] CreatePostDTO createPostDTO)
        {
            var post = new Post
            {
                Title = createPostDTO.Title,
                Content = createPostDTO.Content,
                CreatedAt = DateTime.UtcNow,

                Tags = createPostDTO.NewTags.Select(t => new Tag
                {
                    Name = t.Name,
                    Description = t.Description

                }).ToList()
            };

            var createdPost = _postRepository.Create(post);

            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, MapPostDTO(createdPost));
        }
        [HttpPut("{id}")]
        public ActionResult<PostResponseDTO> Update(int id, [FromBody] CreatePostDTO updatePostDTO)
        {
            var post = _postRepository.GetById(id);

            if (post == null) return NotFound();

            post.Title = updatePostDTO.Title;
            post.Content = updatePostDTO.Content;


            var updatedPost = _postRepository.Update(post);

            return Ok(MapPostDTO(updatedPost));
        }
    }
}