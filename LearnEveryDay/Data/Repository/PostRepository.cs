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

    public async Task<bool> CreatePostAsync(Post post)
    {
      if (post == null)
      {
        throw new ArgumentNullException(nameof(post));
      }

      _context.Posts.Add(post);

      return await SaveChangesAsync();
    }

    public async Task<bool> DeletePostAsync(Post post)
    {
      if (post == null)
      {
        throw new ArgumentNullException(nameof(post));
      }

      _context.Posts.Remove(post);

      return await SaveChangesAsync();
    }

    public async Task<IEnumerable<Post>> GetAllPostsByUserIdAsync(Guid userId)
    {
      if (Guid.Empty == userId)
      {
        throw new ArgumentNullException(nameof(userId));
      }

      // var posts = from post in _context.Posts
      //             select post;

      var posts = _context.Posts.Where(post => post.UserId == userId).OrderByDescending(post => post.PublishedDate);

      return await posts.ToListAsync();
    }

    // @todo fetch logged in user. jwt token? 
    public async Task<IEnumerable<Post>> GetAllPostsByCurrentUserAsync(Guid userId)
    {
      return await GetAllPostsByUserIdAsync(userId);
    }

    public async Task<Post> GetPostByIdAsync(Guid postId)
    {
      return await _context.Posts.FirstOrDefaultAsync(post => post.Id == postId);
    }

    public async Task<Post> GetUserPostByIdAsync(Guid postId, Guid userId)
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

    public async Task<bool> UpdatePostAsync(Post post)
    {
      if (post == null)
      {
        throw new ArgumentNullException(nameof(post));
      }

      _context.Posts.Update(post);

      return await SaveChangesAsync();
    }
  }
}
