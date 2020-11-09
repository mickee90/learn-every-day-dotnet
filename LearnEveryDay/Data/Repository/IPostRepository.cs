using System;
using System.Collections.Generic;
using LearnEveryDay.Models;

namespace LearnEveryDay.Data.Repository
{
  public interface IPostRepository
  {
    bool SaveChanges();

    IEnumerable<Post> GetAllPostsByUserId(Guid UserId);
    IEnumerable<Post> GetAllPostsByCurrentUser(Guid UserId);
    Post GetPostById(Guid id);
    void CreatePost(Post Post);
    void UpdatePost(Post Post);
    void DeletePost(Post Post);

  }
}
