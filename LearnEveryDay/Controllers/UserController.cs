using System;
using System.Collections.Generic;
using AutoMapper;
using LearnEveryDay.Data.Repository;
using LearnEveryDay.Dtos.Post;
using LearnEveryDay.Dtos.User;
using LearnEveryDay.Helpers;
using LearnEveryDay.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LearnEveryDay.Controllers
{
  [Route("api/v1/users")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequestDto model)
    {
      var response = _repository.Authenticate(model);

      if (response == null)
      {
        return BadRequest(new { message = "Username or password is incorrect" });
      }

      return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
      var users = _repository.GetAll();

      return Ok(users);
    }
  }
}
