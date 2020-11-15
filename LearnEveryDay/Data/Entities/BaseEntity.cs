using System;

namespace LearnEveryDay.Data.Entities
{
  public class BaseEntity
  {
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}