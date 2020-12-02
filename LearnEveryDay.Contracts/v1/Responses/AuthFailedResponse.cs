using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Contracts.v1.Responses
{
  public class AuthFailedResponse
  {
    public IEnumerable<string> Errors { get; set; }
  }
}