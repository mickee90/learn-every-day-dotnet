using System;

namespace LearnEveryDay.Contracts.v1.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Boolean Status { get; set; }

        public string Title { get; set; }

        public string Ingress { get; set; }

        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
