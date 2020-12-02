using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public RegisterController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var authResponse = await _repository.RegisterAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            var authUser = _mapper.Map<UserResponse>(authResponse.User);
            authUser.Token = authResponse.Token;
            
            return Ok(authUser);
        }
    }
}