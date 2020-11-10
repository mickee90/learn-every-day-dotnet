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
        return BadRequest(new { message = "Username or password is incorrect" });
      }

      var userReadDto = await _repository.AuthenticateAsync(loginRequest);

      if (userReadDto == null)
      {
        BadRequest(new { message = "Username or password is incorrect" });
      }

      return Ok(userReadDto);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterRequest registerRequest)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      var user = _mapper.Map<User>(registerRequest);

      if (registerRequest.Email != null)
      {
        user.UserName = registerRequest.Email;
      }

      var result = await _userManager.CreateAsync(user, registerRequest.Password);
      if (!result.Succeeded)
      {
        foreach (var error in result.Errors)
        {
          ModelState.TryAddModelError(error.Code, error.Description);
        }

        return BadRequest(new { message = result.Errors });
      }

      await _userManager.AddToRoleAsync(user, "User");

      return Ok();
    }

  }
}
