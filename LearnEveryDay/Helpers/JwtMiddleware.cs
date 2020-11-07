using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnEveryDay.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LearnEveryDay.Helpers
{
  public class JwtMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly AppConfiguration _appConfig;

    public JwtMiddleware(RequestDelegate next, AppConfiguration appConfig)
    {
      _next = next;
      _appConfig = appConfig;
    }

    public async Task Invoke(HttpContext context, IUserRepository userRepository)
    {
      var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

      if (token != null)
      {
        attachUserToContext(context, userRepository, token);
      }

      await _next(context);
    }

    private void attachUserToContext(HttpContext context, IUserRepository userRepository, string token)
    {
      try
      {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appConfig.Secret);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
          // Set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 min later)
          ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        Guid userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        // attach user to context on successful jwt validation
        context.Items["User"] = userRepository.GetUserById(userId);
      }
      catch
      {
        // do nothing if jwt validation fails
        // user is not attached to context so request won't have access to secure routes
      }
    }
  }
}