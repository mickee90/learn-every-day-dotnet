using LearnEveryDay.Data.Entities;

namespace LearnEveryDay.Domain
{
  public class UserResult : ActionResult
  {
    public User User { get; set; }
  }
}