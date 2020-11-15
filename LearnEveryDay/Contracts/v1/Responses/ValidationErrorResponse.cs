using System.Collections.Generic;
using LearnEveryDay.Contracts.V1.Responses;

namespace LearnEveryDay.Contracts.v1.Responses
{
  public class ValidationErrorResponse
  {
    public ValidationErrorResponse(){}

    public ValidationErrorResponse(ErrorModel error)
    {
      Errors.Add(error);
    }
        
    public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
  }
}