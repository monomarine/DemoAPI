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

        public PostResponseDTO Create(CreatePostDTO createPostDTO)
        {
            if (_postRepository.PostExists(createPostDTO.Title))
                throw new ArgumentException($"Пост с именем {createPostDTO.Title} уже существует");

            var newPost = new Post
            {
                Title = createPostDTO.Title,
                Content = createPostDTO.Content,
                CreatedAt = DateTime.UtcNow
            };

            ProcessTagsForPost(newPost, createPostDTO.TagIds, createPostDTO.NewTags);
            var createdPos = _postRepository.Create(newPost);
            return MapToPostDTO(createdPos);
        }

        public bool Delete(int id)=>  _postRepository.Delete(id);

        public IEnumerable<PostResponseDTO> GetAllPosts()
        {
            var posts = _postRepository.GetAll();
            return posts.Select(MapToPostDTO);
        }

        public PostResponseDTO GetById(int id)
        {
            var post = _postRepository.GetById(id);
            if (post != null) return MapToPostDTO(_postRepository.GetById(id));
            else throw new ArgumentException("такого поста не существует");

        }
            


        public PostResponseDTO Update(int id, UpdatePostDTO updatePostDTO)
        {
            var post = _postRepository.GetById(id);
            if (post == null) return null;

            if (post.Title != updatePostDTO.Title &&
                _postRepository.PostExists(updatePostDTO.Title))
                throw new ArgumentException($"пост с заголовком {updatePostDTO.Title}" +
                    $"уже существует");

            post.Title = updatePostDTO.Title;
            post.Content = updatePostDTO.Content;
            post.UpdatedAt = DateTime.UtcNow;

            post.Tags.Clear();
            ProcessTagsForPost(post, updatePostDTO.TagIds, null);

            var updatesPost = _postRepository.Update(post);
            return MapToPostDTO(updatesPost);
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
            //добавляются теги к посту
            foreach (var tag in tagsToAdd)
                post.Tags.Add(tag);
        }
    }
}
