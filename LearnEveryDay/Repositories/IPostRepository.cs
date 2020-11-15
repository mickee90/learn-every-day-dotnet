using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnEveryDay.Data.Entities;

namespace LearnEveryDay.Repositories
{
  public interface IPostRepository
  {
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Post>> GetAllPostsByUserIdAsync(Guid UserId);
    Task<IEnumerable<Post>> GetAllPostsByCurrentUserAsync(Guid UserId);
    Task<Post> GetPostByIdAsync(Guid id);
    Task<Post> GetUserPostByIdAsync(Guid postId, Guid userId);
    Task<bool> CreatePostAsync(Post Post);
    Task<bool> UpdatePostAsync(Post Post);
    Task<bool> DeletePostAsync(Post Post);
  }
}
