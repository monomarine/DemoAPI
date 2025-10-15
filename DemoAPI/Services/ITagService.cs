using DemoAPI.Models;
using DemoAPI.Models.DTO;

namespace DemoAPI.Services
{
    public interface ITagService
    {
        IEnumerable<TagResponseDTO> GetAllTags();
        TagResponseDTO GetById(int id);
        TagResponseDTO Create(CreateTagDTO createTagDTO);
        TagResponseDTO Update(int id, UpdateTagDTO updatetagDTO);
        bool Delete(int id);
    }
}
