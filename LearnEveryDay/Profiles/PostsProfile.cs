using System;
using AutoMapper;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Entities;

namespace LearnEveryDay.Profiles
{
  public class PostsProfile : Profile
  {
    public PostsProfile()
    {
      // Source -> Target
      CreateMap<Post, PostResponse>();
      CreateMap<CreatePostRequest, Post>();
      CreateMap<UpdatePostRequest, Post>();
      CreateMap<Post, UpdatePostRequest>();
    }
  }
}
