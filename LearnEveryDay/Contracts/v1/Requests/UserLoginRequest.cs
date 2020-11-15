using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Contracts.v1.Requests
{
  public class UserLoginRequest
  {
    public string UserName { get; set; }

    public string Password { get; set; }
  }
}