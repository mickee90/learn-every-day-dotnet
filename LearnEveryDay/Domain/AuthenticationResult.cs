using System.Collections.Generic;
using LearnEveryDay.Data.Entities;

namespace LearnEveryDay.Domain
{
  public class AuthenticationResult : RepositoryResult
  {
    public User User { get; set; }
    public string Token { get; set; }
  }
}