using System;
using System.Collections.Generic;
using System.Linq;
using LearnEveryDay.Models;

namespace LearnEveryDay.Data.Repository
{
  public class PostRepository : IPostRepository
  {
    private readonly AppDbContext _context;
    private readonly AppConfiguration _appConfig;

    public PostRepository(AppDbContext context, AppConfiguration appConfig)
    {
      _context = context;
      _appConfig = appConfig;
    }

    public void CreatePost(Post Post)
    {
      if (Post == null)
      {
        throw new ArgumentNullException(nameof(Post));
      }

      _context.Posts.Add(Post);
    }

    public void DeletePost(Post Post)
    {
      if (Post == null)
      {
        throw new ArgumentNullException(nameof(Post));
      }

      _context.Posts.Remove(Post);
    }

    public IEnumerable<Post> GetAllPostsByUserId(Guid id)
    {
      if (Guid.Empty == id)
      {
        throw new ArgumentNullException(nameof(id));
      }

      var posts = from post in _context.Posts
                  select post;

      posts.Where(post => post.UserId == id).OrderByDescending(post => post.PublishedDate);

      return posts.ToList();
    }

    // @todo fetch logged in user. jwt token? 
    public IEnumerable<Post> GetAllPostsByCurrentUser()
    {
      return GetAllPostsByUserId(_appConfig.MockUserId);
    }

    public Post GetPostById(Guid id)
    {
      return _context.Posts.FirstOrDefault(post => post.Id == id);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }

    public void UpdatePost(Post Post)
    {
      if (Post == null)
      {
        throw new ArgumentNullException(nameof(Post));
      }

      _context.Posts.Update(Post);
    }
  }
}
