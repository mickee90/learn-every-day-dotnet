﻿using System;

namespace LearnEveryDay.Dtos.Post
{
    public class PostReadDto
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
