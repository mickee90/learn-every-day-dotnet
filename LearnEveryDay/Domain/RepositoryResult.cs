
using System.Collections.Generic;

namespace LearnEveryDay.Domain
{
  public class RepositoryResult
  {
    public RepositoryResult()
    {

    }

    public bool Success { get; set; }

    public IEnumerable<string> Errors { get; set; }
  }

}