using LearnEveryDay.Entities;

namespace LearnEveryDay.Domain
{
  public class UserResult : RepositoryResult
  {
    public User User { get; set; }
  }
}