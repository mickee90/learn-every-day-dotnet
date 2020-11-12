using System.Threading.Tasks;
using AutoMapper;
using LearnEveryDay.Data.Repository;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnEveryDay.Controllers
{
  [Route("api/v1/users")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserController(UserManager<User> userManager, IUserRepository repository, IMapper mapper)
    {
      _userManager = userManager;
      _repository = repository;
      _mapper = mapper;
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
