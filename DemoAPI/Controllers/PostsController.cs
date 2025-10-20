using DemoAPI.Models;
using DemoAPI.Models.DTO;
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

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        private static PostResponseDTO MapPostsDTO (Post post)
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
            var postsDTO = posts.Select(MapPostsDTO);
            return Ok(MapPostsDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponseDTO> GetById(int id)
        {
            var posts = _postRepository.GetById(id);
            if (posts == null) return NotFound();

            return Ok(MapPostsDTO(posts));
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

            return CreatedAtAction(nameof(GetById), new
            {
                id = createdPost.Id
            },
            MapPostsDTO(createdPost));
        }


        [HttpPut("{id}")]
        public ActionResult<PostResponseDTO> Update(int id, [FromBody] CreatePostDTO updatePostDTO)
        {
            var post = _postRepository.GetById(id);

            if (post == null) return NotFound();

            post.Title = updatePostDTO.Title;
            post.Content = updatePostDTO.Content;

            var updatedPost = _postRepository.Update(post);

            return Ok(MapPostsDTO(updatedPost));
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
