using System.Collections.Generic;

namespace LearnEveryDay.Contracts.v1.Responses
{
    public class PostsResponse
    {
        public PostsResponse() {}

        public PostsResponse(IEnumerable<PostResponse> data)
        {
            Data = data;
        }
        
        public IEnumerable<PostResponse> Data { get; set; }
    }
}
