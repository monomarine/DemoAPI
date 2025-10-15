using DemoAPI.Models;
using DemoAPI.Models.DTO;

namespace DemoAPI.Services
{
    public interface IPostService
    {
        IEnumerable<PostResponseDTO> GetAllPosts();
        PostResponseDTO GetById(int id);
        PostResponseDTO Create(CreatePostDTO createPostDTO);
        PostResponseDTO Update(int id, UpdatePostDTO updatePostDTO);
        bool Delete(int id);

    }
}
