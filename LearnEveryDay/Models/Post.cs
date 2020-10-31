using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LearnEveryDay.Models
{
    public class Post : BaseEntity
    {

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [DefaultValue(false)]
        public Boolean Status { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Ingress { get; set; }

        // @todo change to proper type
        [Required]
        public byte[] Content { get; set; }

        [Required]
        [DefaultValue(false)]
        public Boolean Deleted { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

    }
}
