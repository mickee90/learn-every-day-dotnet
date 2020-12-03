using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Data.Entities;
using LearnEveryDay.Domain;
using LearnEveryDay.Repositories;
using Microsoft.AspNetCore.Http;

namespace LearnEveryDay.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        
        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Post> CreatePostAsync(Post post, Guid userId)
        {
            post.PublishedDate = post.PublishedDate != null ? post.PublishedDate : DateTime.Now;

            var response = await _repository.CreatePostAsync(post);

            return response ? post : null;
        }

        public async Task<Post> GetUserPostByIdAsync(Guid postId, Guid userId)
        {
            return await _repository.GetUserPostByIdAsync(postId, userId);
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            return await _repository.GetPostsByUserIdAsync(userId);
        }
        
        public async Task<ActionResult> UpdatePostAsync(Guid postId, Guid userId, UpdatePostRequest request)
        {
            var post = await _repository.GetUserPostByIdAsync(postId, userId);

            if (request == null)
            {
                return new ActionResult
                {
                    Errors = new[] { "The request data is missing." }
                };
            }

            if (post == null)
            {
                return new ActionResult
                {
                    Errors = new[] { "The post could not be found." }
                };
            }
            
            post.Title = request.Title;
            post.Ingress = request.Ingress;
            post.PublishedDate = request.PublishedDate ?? DateTime.Now;
            post.Content = request.Content;

            var response = await _repository.UpdatePostAsync(post);
      
            if (!response)
            {
                return new ActionResult
                {
                    Errors = new[] { "The post could not be updated." }
                };
            }
            
            return new ActionResult { Success = true };
        }
    }
}