using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Data.Entities;
using LearnEveryDay.Domain;
using LearnEveryDay.Repositories;

namespace LearnEveryDay.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        
        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<ActionResult> UpdatePostAsync(Post post, UpdatePostRequest request)
        {
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
            post.PublishedDate = request.PublishedDate;
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