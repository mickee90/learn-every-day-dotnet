using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnEveryDay.Models
{
  public class Post : BaseEntity
  {

    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [Required]
    public Boolean Status { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string Ingress { get; set; }

    // @todo change to proper type
    [Required]
    [MaxLength(255)]
    public string Content { get; set; }

    [Required]
    public Boolean Deleted { get; set; }

    [Required]
    public DateTime PublishedDate { get; set; }

  }
}
