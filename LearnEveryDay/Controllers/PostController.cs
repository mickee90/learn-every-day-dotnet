using System;
using System.Collections.Generic;
using AutoMapper;
using LearnEveryDay.Data.Repository;
using LearnEveryDay.Dtos.Post;
using LearnEveryDay.Helpers;
using LearnEveryDay.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LearnEveryDay.Controllers
{
  [Route("api/v1/posts")]
  [ApiController]
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
    //[Authorize]
    [HttpGet]
    public ActionResult<IEnumerable<PostReadDto>> GetAllPosts()
    {
      var posts = _repository.GetAllPostsByCurrentUser();

      return Ok(_mapper.Map<IEnumerable<PostReadDto>>(posts));
    }

    // api/v1/posts/{id}
    [HttpGet("{id}", Name = "GetPostById")]
    public ActionResult<PostReadDto> GetPostById(Guid id)
    {
      var post = _repository.GetPostById(id);

      if (post == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<PostReadDto>(post));
    }

    // api/v1/posts
    [HttpPost]
    public ActionResult<PostReadDto> CreatePost(PostCreateDto postCreateDto)
    {
      var post = _mapper.Map<Post>(postCreateDto);
      _repository.CreatePost(post);
      _repository.SaveChanges();

      var postReadDto = _mapper.Map<PostReadDto>(post);

      return CreatedAtRoute(nameof(GetPostById), new { postReadDto.Id }, postReadDto);
    }

    // api/v1/posts/{id}
    [HttpPatch("{id}")]
    public ActionResult PatchPost(Guid id, JsonPatchDocument<PostUpdateDto> patchDoc)
    {
      if (patchDoc == null)
      {
        return BadRequest();
      }

      var post = _repository.GetPostById(id);

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
