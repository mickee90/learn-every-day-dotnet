using System;
using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Contracts.v1.Requests
{
    public class UpdatePostRequest
    {
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
        public DateTime? PublishedDate { get; set; }
    }
}
