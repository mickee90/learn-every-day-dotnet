using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEveryDay.Data;
using LearnEveryDay.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnEveryDay.Repositories
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

    public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId)
    {
      if (Guid.Empty == userId)
      {
        throw new UnauthorizedAccessException(nameof(userId));
      }

      var posts = _context.Posts.Where(post => post.UserId == userId).OrderByDescending(post => post.PublishedDate);

      return await posts.ToListAsync();
    }

    public async Task<Post> GetPostByIdAsync(Guid postId)
    {
      return await _context.Posts.FirstOrDefaultAsync(post => post.Id == postId);
    }

    public async Task<Post> GetUserPostByIdAsync(Guid postId, Guid userId)
    {
      if (Guid.Empty == userId)
      {
        throw new UnauthorizedAccessException(nameof(userId));
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
