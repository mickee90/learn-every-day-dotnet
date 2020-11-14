using System.Threading.Tasks;
using LearnEveryDay.Data.Repository;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEveryDay.Controllers
{
  [Route("api/v1/users")]
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class UserController : ControllerBase
  {
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
      _repository = repository;
    }

    [HttpPut("UpdateUser")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest updateUserRequest)
    {
      if (!ModelState.IsValid)
      {
        // @todo add better response information
        return BadRequest(new UserFailedResponse
        {
          Errors = new[] {"Enter the required fields"}
        });
      }

      var userResponse = await _repository.UpdateUserAsync(updateUserRequest, HttpContext.GetUserId());

      if (!userResponse.Success)
      {
        return BadRequest(new UserFailedResponse
        {
          Errors = userResponse.Errors
        });
      }

      return Ok(new UserResponse(userResponse.User, null));
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(UserLoginRequest loginRequest)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(new AuthFailedResponse
        {
          Errors = new[] { "Username or password is incorrect" }
        });
      }

      var authResponse = await _repository.AuthenticateAsync(loginRequest);

      if (!authResponse.Success)
      {
        return BadRequest(new AuthFailedResponse
        {
          Errors = authResponse.Errors
        });
      }

      return Ok(new UserResponse(authResponse.User, authResponse.Token));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterRequest registerRequest)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(new AuthFailedResponse
        {
          Errors = new[] { "Please enter all the required fields" }
        });
      }

      var authResponse = await _repository.RegisterAsync(registerRequest);

      if (!authResponse.Success)
      {
        return BadRequest(new AuthFailedResponse
        {
          Errors = authResponse.Errors
        });
      }

      return Ok(new UserResponse(authResponse.User, authResponse.Token));
    }
  }
}
