using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Contracts.v1.Requests
{
  public class UpdateUserRequest
  {
    [Required]
    [EmailAddress]
    public string UserName { get; set; }

    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(255)]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [MaxLength(255)]
    public string Address { get; set; }

    [MaxLength(255)]
    public string ZipCode { get; set; }

    [MaxLength(255)]
    public string City { get; set; }

    [MaxLength(255)]
    [Phone]
    public string Phone { get; set; }
  }
}