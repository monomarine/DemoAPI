using System.Collections;
using DemoAPI.Models;

namespace DemoAPI.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Tag? GetByName(string name);
        IEnumerable<Tag> GetTagsByNames(params string[] names);
        bool TagExists(string name);
    }
}
