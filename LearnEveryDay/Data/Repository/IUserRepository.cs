using System;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Domain;
using LearnEveryDay.Entities;

namespace LearnEveryDay.Data.Repository
{
  public interface IUserRepository
  {
    Task<bool> SaveChangesAsync();

    Task<User> GetUserByIdAsync(Guid id);
    Task<UserResult> UpdateUserAsync(UpdateUserRequest updateUserRequest, Guid userId);
    Task<AuthenticationResult> AuthenticateAsync(UserLoginRequest authRequest);
    Task<AuthenticationResult> RegisterAsync(UserRegisterRequest registerRequest);
  }
}
