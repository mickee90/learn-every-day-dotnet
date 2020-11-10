using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEveryDay.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> CreatePostAsync(Post Post)
    {
      if (Post == null)
      {
        throw new ArgumentNullException(nameof(Post));
      }

      _context.Posts.Add(Post);

      return await SaveChangesAsync();
    }

    public async Task<bool> DeletePostAsync(Post Post)
    {
      if (Post == null)
      {
        throw new ArgumentNullException(nameof(Post));
      }

      _context.Posts.Remove(Post);

      return await SaveChangesAsync();
    }

    public async Task<IEnumerable<Post>> GetAllPostsByUserIdAsync(Guid UserId)
    {
      if (Guid.Empty == UserId)
      {
        throw new ArgumentNullException(nameof(UserId));
      }

      var posts = from post in _context.Posts
                  select post;

      posts.Where(post => post.UserId == UserId).OrderByDescending(post => post.PublishedDate);

      return await posts.ToListAsync();
    }

    // @todo fetch logged in user. jwt token? 
    public async Task<IEnumerable<Post>> GetAllPostsByCurrentUserAsync(Guid UserId)
    {
      return await GetAllPostsByUserIdAsync(UserId);
    }

    public async Task<Post> GetPostByIdAsync(Guid postId)
    {
      return await _context.Posts.FirstOrDefaultAsync(post => post.Id == postId);
    }

    public async Task<Post> GetUserPostByIdAsync(Guid userId, Guid postId)
    {
      if (Guid.Empty == userId)
      {
        throw new ArgumentNullException(nameof(userId));
      }

      if (Guid.Empty == postId)
      {
        throw new ArgumentNullException(nameof(postId));
      }

      return await _context.Posts.FirstOrDefaultAsync(post => post.Id == postId && post.UserId == userId);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync() >= 0);
    }

    public async Task<bool> UpdatePostAsync(Post Post)
    {
      if (Post == null)
      {
        throw new ArgumentNullException(nameof(Post));
      }

      _context.Posts.Update(Post);

      return await SaveChangesAsync();
    }
  }
}
