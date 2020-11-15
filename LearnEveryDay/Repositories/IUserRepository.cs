using System;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Domain;
using LearnEveryDay.Data.Entities;

namespace LearnEveryDay.Repositories
{
  public interface IUserRepository
  {
    Task<bool> SaveChangesAsync();

    Task<User> GetUserByIdAsync(Guid id);
    Task<UserResult> UpdateUserAsync(User user);
    Task<UserResult> UpdatePasswordAsync(User user, string password);
    Task<AuthenticationResult> AuthenticateAsync(UserLoginRequest authRequest);
    Task<AuthenticationResult> RegisterAsync(UserRegisterRequest registerRequest);
  }
}
