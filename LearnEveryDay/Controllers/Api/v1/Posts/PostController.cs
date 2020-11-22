using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LearnEveryDay.Repositories;
using LearnEveryDay.Extensions;
using LearnEveryDay.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Services;

namespace LearnEveryDay.Controllers.Api.v1.Posts
{
  [Route("api/v1/posts")]
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class PostController : ControllerBase
  {
    private readonly IPostRepository _repository;
    private readonly IPostService _service;
    private readonly IMapper _mapper;

    public PostController(IPostRepository repository, IMapper mapper, IPostService service)
    {
      _repository = repository;
      _mapper = mapper;
      _service = service;
    }

    // api/v1/posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostResponse>>> Index()
    {
      var posts = await _repository.GetAllPostsByCurrentUserAsync(HttpContext.GetUserId());

      var postResponse = _mapper.Map<IEnumerable<Post>>(posts);

      return Ok(new PostsResponse(postResponse));
    }

    // api/v1/posts/{postId}
    [HttpGet("{postId}", Name="Get")]
    public async Task<ActionResult<PostResponse>> Get(Guid postId)
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
    public async Task<ActionResult<PostResponse>> Create(CreatePostRequest createRequest)
    {
      createRequest.UserId = HttpContext.GetUserId();
      var post = _mapper.Map<Post>(createRequest);
      
      post.PublishedDate = post.PublishedDate != null ? post.PublishedDate : DateTime.Now;

      await _repository.CreatePostAsync(post);

      var postResponse = _mapper.Map<PostResponse>(post);

      return CreatedAtRoute(nameof(Get), new { postId = postResponse.Id }, postResponse);
    }

    // api/v1/posts/{postId}
    [HttpPut("{postId}")]
    public async Task<ActionResult> Update([FromRoute]Guid postId, [FromBody] UpdatePostRequest request)
    {
      if (request == null)
      {
        return BadRequest();
      }

      var post = await _repository.GetUserPostByIdAsync(postId, HttpContext.GetUserId());

      if (post == null)
      {
        return NotFound();
      }

      var response = await _service.UpdatePostAsync(post, request);
      
      if (!response.Success)
      {
        return BadRequest(new ErrorResponse
        {
          Errors = response.Errors.ToList()
        });
      }

      return NoContent();
    }
  }
}
