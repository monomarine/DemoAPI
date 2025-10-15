using DemoAPI.Models;
using DemoAPI.Models.DTO;
using DemoAPI.Repositories;

namespace DemoAPI.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository repo)
        {
            _tagRepository = repo;
        }
        private static TagResponseDTO MapToTagDTO(Tag tag)
        {
            return new TagResponseDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description
            };
        }

        public TagResponseDTO Create(CreateTagDTO createTagDTO)
        {
            if (_tagRepository.TagExists(createTagDTO.Name))
                throw new ArgumentException("тег уже существует");

            var tag = new Tag
            {
                Name = createTagDTO.Name,
                Description = createTagDTO.Description
            };

            var createdTag = _tagRepository.Create(tag);
            return MapToTagDTO(createdTag);
        }

        public bool Delete(int id) => _tagRepository.Delete(id);

        public IEnumerable<TagResponseDTO> GetAllTags() =>
            _tagRepository.GetAll().Select(MapToTagDTO);

        public TagResponseDTO GetById(int id)
        {
            #pragma warning disable
            var tag = _tagRepository.GetById(id);
            return tag == null ? null : MapToTagDTO(tag);
        }

        public TagResponseDTO Update(int id, UpdateTagDTO updatetagDTO)
        {
            var tag = _tagRepository.GetById(id);
            if (tag == null) return null;

            if (tag.Name != updatetagDTO.Name &&
                _tagRepository.TagExists(updatetagDTO.Name))
                throw new ArgumentException("тег уже существует");

            tag.Name = updatetagDTO.Name;
            tag.Description = updatetagDTO.Description;

            var updatesTag = _tagRepository.Update(tag);
            return MapToTagDTO(updatesTag);
        }
    }
}
