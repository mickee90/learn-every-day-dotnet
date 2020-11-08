using System;
using AutoMapper;
using LearnEveryDay.Dtos.User;
using LearnEveryDay.Models;

namespace LearnEveryDay.Profiles
{
  public class UsersProfile : Profile
  {
    public UsersProfile()
    {
      // Source -> Target
      CreateMap<User, UserReadDto>();
      CreateMap<UserRegistrationDto, User>();
    }
  }
}
