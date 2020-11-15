using System.Linq;
using System.Threading.Tasks;
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
    public class UpdatePasswordController : ControllerBase
    {
        private readonly IUserService _service;


        public UpdatePasswordController(IUserService service)
        {
            _service = service;
        }

        [HttpPut("update_password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
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

            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new[] {"The passwords do not match."}
                });
            }

            var passwordResponse = await _service.UpdatePasswordAsync(HttpContext.GetUserId(), request);
            
            if (!passwordResponse.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = passwordResponse.Errors
                });
            }

            return Ok();
        }
    }
}