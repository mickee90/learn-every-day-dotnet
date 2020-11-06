using System;

namespace LearnEveryDay
{
  public class AppConfiguration
  {
    public string MysqlUser { get; set; }
    public string MysqlPassword { get; set; }
    public string MysqlDatabase { get; set; }
    public string ConnectionStrings { get; set; }
    public Guid MockUserId { get; set; }
    public string Secret { get; set; }
  }
}
