using DemoAPI.Models.DTO;

namespace DemoAPI.Services
{
    public interface ITagService
    {
        IEnumerable<TagResponseDTO> GetAll();
        TagResponseDTO GetById(int id);
        TagResponseDTO Create(CreateTagDTO createTagDTO);
        TagResponseDTO Update(int id, CreateTagDTO updatetagDTO);
        bool Delete(int id);
    }
}
