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

    public IEnumerable<Post> GetAllPostsByUserId(Guid UserId)
    {
      if (Guid.Empty == UserId)
      {
        throw new ArgumentNullException(nameof(UserId));
      }

      var posts = from post in _context.Posts
                  select post;

      posts.Where(post => post.UserId == UserId).OrderByDescending(post => post.PublishedDate);

      return posts.ToList();
    }

    // @todo fetch logged in user. jwt token? 
    public IEnumerable<Post> GetAllPostsByCurrentUser(Guid UserId)
    {
      return GetAllPostsByUserId(UserId);
    }

    public Post GetPostById(Guid postId)
    {
      return _context.Posts.FirstOrDefault(post => post.Id == postId);
    }

    public Post GetUserPostById(Guid userId, Guid postId)
    {
      if (Guid.Empty == userId)
      {
        throw new ArgumentNullException(nameof(userId));
      }

      if (Guid.Empty == postId)
      {
        throw new ArgumentNullException(nameof(postId));
      }

      return _context.Posts.FirstOrDefault(post => post.Id == postId && post.UserId == userId);
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
