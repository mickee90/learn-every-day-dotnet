using System;
using System.Collections.Generic;
using AutoMapper;
using LearnEveryDay.Data.Repository;
using LearnEveryDay.Dtos.Post;
using LearnEveryDay.Extensions;
using LearnEveryDay.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LearnEveryDay.Controllers
{
  [Route("api/v1/posts")]
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class PostController : ControllerBase
  {
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public PostController(IPostRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    // api/v1/posts
    [HttpGet]
    public ActionResult<IEnumerable<PostReadDto>> GetAllPosts()
    {
      var posts = _repository.GetAllPostsByCurrentUser(HttpContext.GetUserId());

      return Ok(_mapper.Map<IEnumerable<PostReadDto>>(posts));
    }

    // api/v1/posts/{id}
    [HttpGet("{id}", Name = "GetPostById")]
    public ActionResult<PostReadDto> GetPostById(Guid postId)
    {
      var post = _repository.GetUserPostById(postId, HttpContext.GetUserId());

      if (post == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<PostReadDto>(post));
    }

    // api/v1/posts
    [HttpPost]
    [Authorize]
    public ActionResult<PostReadDto> CreatePost(PostCreateDto postCreateDto)
    {
      postCreateDto.UserId = HttpContext.GetUserId();
      var post = _mapper.Map<Post>(postCreateDto);

      _repository.CreatePost(post);
      _repository.SaveChanges();

      var postReadDto = _mapper.Map<PostReadDto>(post);

      return CreatedAtRoute(nameof(GetPostById), new { postReadDto.Id }, postReadDto);
    }

    // api/v1/posts/{id}
    [HttpPatch("{postId}")]
    public ActionResult PatchPost(Guid postId, JsonPatchDocument<PostUpdateDto> patchDoc)
    {
      if (patchDoc == null)
      {
        return BadRequest();
      }

      var post = _repository.GetUserPostById(postId, HttpContext.GetUserId());

      if (post == null)
      {
        return NotFound();
      }

      var postToPatch = _mapper.Map<PostUpdateDto>(post);
      patchDoc.ApplyTo(postToPatch, ModelState);

      if (!TryValidateModel(postToPatch))
      {
        return ValidationProblem(ModelState);
      }

      _mapper.Map(postToPatch, post);

      _repository.UpdatePost(post);

      _repository.SaveChanges();


      return NoContent();
    }
  }
}
