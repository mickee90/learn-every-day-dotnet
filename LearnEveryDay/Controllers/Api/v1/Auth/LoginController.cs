using System.Threading.Tasks;
using LearnEveryDay.Repositories;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Contracts.v1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LearnEveryDay.Controllers.Api.v1.Auth
{
    [Route("api/v1/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public LoginController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] {"Username or password is incorrect"}
                });
            }

            var authResponse = await _repository.AuthenticateAsync(request);

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