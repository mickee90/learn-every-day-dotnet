using System.Collections.Generic;

namespace LearnEveryDay.Contracts.v1.Responses
{
  public class ErrorResponse
  {
    public ErrorResponse(){}

    public ErrorResponse(string error)
    {
      Errors.Add(error);
    }
    
    public List<string> Errors { get; set; }
  }
}