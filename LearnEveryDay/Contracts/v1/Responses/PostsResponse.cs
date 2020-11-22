using System.Collections.Generic;
using LearnEveryDay.Data.Entities;

namespace LearnEveryDay.Contracts.v1.Responses
{
    public class PostsResponse
    {
        public PostsResponse() {}

        public PostsResponse(IEnumerable<Post> data)
        {
            Data = data;
        }
        
        public IEnumerable<Post> Data { get; set; }
    }
}
