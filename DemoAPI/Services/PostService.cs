using DemoAPI.Models;
using DemoAPI.Models.DTO;
using DemoAPI.Repositories;

namespace DemoAPI.Services
{
    public class PostService : IPostService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;

        public PostService(ITagRepository tagrepo, IPostRepository postrepo)
        {
            _tagRepository = tagrepo;
            _postRepository = postrepo;
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

        private void ProcessTagsForPost(Post post, 
                                    List<int>? tagsIds,
                                    List<CreateTagDTO>? newTags)
        {
            var tagsToAdd = new List<Tag>(); //добавляемые теги к посту

            //добавление существующих тегов
            if(tagsIds != null && tagsIds.Any())
            {
                foreach(var tagId in tagsIds)
                {
                    var existingtag = _tagRepository.GetById(tagId);
                    if (existingtag != null)
                        tagsToAdd.Add(existingtag);
                }
            }
            //создание новых тегов
            else if(newTags != null && newTags.Any())
            {
                foreach(var newTag in newTags)
                {
                    var existingTag = _tagRepository.GetByName(newTag.Name);
                    if(existingTag != null)
                    {
                        tagsToAdd.Add(existingTag);
                    }
                    else
                    {
                        var newTagForAdd = new Tag
                        {
                            Name = newTag.Name,
                            Description = newTag.Description
                        };

                        var creatingTag = _tagRepository.Create(newTagForAdd);
                        tagsToAdd.Add(creatingTag);
                    }
                }
            }

            foreach (var tag in tagsToAdd)
                post.Tags.Add(tag);
        }
    }
}
