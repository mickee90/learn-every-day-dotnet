using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Dtos.User
{
  public class UserRegistrationDto
  {
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }
  }
}