using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Contracts.v1.Responses
{
  public class UserLoginResponse
  {
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
  }
}