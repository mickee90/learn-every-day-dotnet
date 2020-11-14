using System.Collections;
using System.Linq;
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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPut("UpdatePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = ModelState.Select(x => x.Value.Errors)
                        .Where(y => y.Count > 0)
                        .Cast<string>()
                });
            }

            if (updatePasswordRequest.Password != updatePasswordRequest.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new[] {"The passwords do not match."}
                });
            }

            var passwordResponse =
                await _repository.UpdatePasswordAsync(updatePasswordRequest, HttpContext.GetUserId());
            
            if (!passwordResponse.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = passwordResponse.Errors
                });
            }

            return Ok();
        }

        [HttpPut("UpdateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = ModelState.Select(x => x.Value.Errors)
                        .Where(y => y.Count > 0)
                        .Cast<string>()
                });
            }

            var userResponse = await _repository.UpdateUserAsync(updateUserRequest, HttpContext.GetUserId());

            if (!userResponse.Success)
            {
                return BadRequest(new ErrorResponse
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
                    Errors = new[] {"Username or password is incorrect"}
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
                    Errors = new[] {"Please enter all the required fields"}
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