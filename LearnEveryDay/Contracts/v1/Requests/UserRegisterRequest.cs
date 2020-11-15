using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Contracts.v1.Requests
{
  public class UserRegisterRequest
  {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
  }
}