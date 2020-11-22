using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Data.Entities;
using LearnEveryDay.Domain;

namespace LearnEveryDay.Services
{
    public interface IPostService
    {
        public Task<ActionResult> UpdatePostAsync(Guid postId, Guid userId, UpdatePostRequest request);
        public Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId);
        public Task<Post> GetUserPostByIdAsync(Guid postId, Guid userId);
        public Task<Post> CreatePostAsync(Post post, Guid userId);
    }
}