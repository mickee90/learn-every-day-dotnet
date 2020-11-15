using System.Threading.Tasks;
using LearnEveryDay.Repositories;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Contracts.v1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LearnEveryDay.Controllers.Api.v1.Auth
{
    [Route("api/v1/auth")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public RegisterController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] {"Please enter all the required fields"}
                });
            }

            var authResponse = await _repository.RegisterAsync(request);

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