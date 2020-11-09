using System;
using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Dtos.Post
{
    public class PostCreateDto
    {
        public PostCreateDto(AppConfiguration appConfig)
        {
            // @todo Check why appConfig does not work
            // @todo Replace hardcoded Guid with authenticated user Guid
            UserId = Guid.Parse("6d9a6f69-30b7-4c23-ac31-9d03a6896a89");
        }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Boolean Status { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Ingress { get; set; }

        // @todo change type
        [Required]
        [MaxLength(255)]
        public string Content { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }
    }
}
