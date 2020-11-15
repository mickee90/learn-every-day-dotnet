using AutoMapper;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Data.Entities;

namespace LearnEveryDay.Profiles
{
  public class UsersProfile : Profile
  {
    public UsersProfile()
    {
      // Source -> Target
      CreateMap<User, UserResponse>();
      CreateMap<UserRegisterRequest, User>();
    }
  }
}
