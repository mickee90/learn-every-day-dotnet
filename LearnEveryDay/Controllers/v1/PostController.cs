using System;
using System.Collections.Generic;
using AutoMapper;
using LearnEveryDay.Data.Repository;
using LearnEveryDay.Extensions;
using LearnEveryDay.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Contracts.v1.Requests;

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
    public async Task<ActionResult<IEnumerable<PostResponse>>> GetAllPosts()
    {
      var posts = await _repository.GetAllPostsByCurrentUserAsync(HttpContext.GetUserId());

      return Ok(_mapper.Map<IEnumerable<PostResponse>>(posts));
    }

    // api/v1/posts/{id}
    [HttpGet("{postId}", Name = "GetPostById")]
    public async Task<ActionResult<PostResponse>> GetPostById(Guid postId)
    {
      var post = await _repository.GetUserPostByIdAsync(postId, HttpContext.GetUserId());

      if (post == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<PostResponse>(post));
    }

    // api/v1/posts
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PostResponse>> CreatePost(CreatePostRequest createRequest)
    {
      createRequest.UserId = HttpContext.GetUserId();
      var post = _mapper.Map<Post>(createRequest);

      await _repository.CreatePostAsync(post);

      var postResponse = _mapper.Map<PostResponse>(post);

      return CreatedAtRoute(nameof(GetPostById), new { postId = postResponse.Id }, postResponse);
    }

    // api/v1/posts/{id}
    [HttpPatch("{postId}")]
    public async Task<ActionResult> PatchPost(Guid postId, JsonPatchDocument<UpdatePostRequest> patchDoc)
    {
      if (patchDoc == null)
      {
        return BadRequest();
      }

      var post = await _repository.GetUserPostByIdAsync(postId, HttpContext.GetUserId());

      if (post == null)
      {
        return NotFound();
      }

      var postToPatch = _mapper.Map<UpdatePostRequest>(post);
      patchDoc.ApplyTo(postToPatch, ModelState);

      if (!TryValidateModel(postToPatch))
      {
        return ValidationProblem(ModelState);
      }

      _mapper.Map(postToPatch, post);

      await _repository.UpdatePostAsync(post);

      return NoContent();
    }
  }
}
