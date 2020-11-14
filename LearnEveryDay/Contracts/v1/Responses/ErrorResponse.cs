using System.Collections.Generic;

namespace LearnEveryDay.Contracts.v1.Responses
{
  public class ErrorResponse
  {
    public IEnumerable<string> Errors { get; set; }
  }
}