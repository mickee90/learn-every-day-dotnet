using System;
using System.Collections.Generic;
using LearnEveryDay.Models;

namespace LearnEveryDay.Data.Repository
{
  public interface IPostRepository
  {
    bool SaveChanges();

    IEnumerable<Post> GetAllPostsByUserId(Guid id);
    IEnumerable<Post> GetAllPostsByCurrentUser();
    Post GetPostById(Guid id);
    void CreatePost(Post Post);
    void UpdatePost(Post Post);
    void DeletePost(Post Post);

  }
}
