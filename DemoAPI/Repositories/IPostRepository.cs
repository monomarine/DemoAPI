using DemoAPI.Models;

namespace DemoAPI.Repositories;


public interface IPostRepository :IRepository<Post>
{
    IEnumerable<Post> GetPostsByTags(int tagId);
    bool PostExists(string title);
}
