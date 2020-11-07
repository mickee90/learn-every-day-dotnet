using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Dtos.User
{
  public class AuthenticateRequestDto
  {
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
  }
}