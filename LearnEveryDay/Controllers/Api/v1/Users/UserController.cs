using System.Linq;
using System.Threading.Tasks;
using LearnEveryDay.Repositories;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Extensions;
using LearnEveryDay.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEveryDay.Controllers.Api.v1.Users
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _service;

        public UserController(IUserRepository repository, IUserService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            var existingUser = await _repository.GetUserByIdAsync(HttpContext.GetUserId());
      
            if (existingUser == null)
            {
                return BadRequest(new ErrorResponse("The user could not be found."));
            }

            var response = await _service.UpdateUserAsync(existingUser, request);

            if (!response.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = response.Errors.ToList()
                });
            }

            return Ok(new UserResponse(response.User, null));
        }
    }
}