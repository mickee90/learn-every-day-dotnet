using System;
using AutoMapper;
using LearnEveryDay.Dtos.Post;
using LearnEveryDay.Models;

namespace LearnEveryDay.Profiles
{
  public class PostsProfile : Profile
  {
    public PostsProfile()
    {
      // Source -> Target
      CreateMap<Post, PostReadDto>();
      CreateMap<PostCreateDto, Post>();
      CreateMap<PostUpdateDto, Post>();
      CreateMap<Post, PostUpdateDto>();
    }
  }
}
