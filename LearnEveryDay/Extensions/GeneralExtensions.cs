using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace LearnEveryDay.Extensions
{
  public static class GeneralExtensions
  {
    public static Guid GetUserId(this HttpContext httpContext)
    {
      if (httpContext.User == null)
      {
        return Guid.Empty;
      }

      return Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);
    }
  }
}