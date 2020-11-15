using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Data.Entities;
using LearnEveryDay.Domain;

namespace LearnEveryDay.Services
{
    public interface IPostService
    {
        public Task<ActionResult> UpdatePostAsync(Post post, UpdatePostRequest request);
    }
}