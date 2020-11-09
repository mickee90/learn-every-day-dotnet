using System;
using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Dtos.Post
{
  public class PostCreateDto
  {
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
