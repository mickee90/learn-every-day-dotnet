using System;
using System.Threading.Tasks;
using LearnEveryDay.Dtos.User;
using LearnEveryDay.Models;

namespace LearnEveryDay.Data.Repository
{
  public interface IUserRepository
  {
    Task<bool> SaveChangesAsync();

    Task<User> GetUserByIdAsync(Guid id);
    Task<bool> UpdateUserAsync(User User);
    Task<UserReadDto> AuthenticateAsync(AuthenticateRequestDto userDto);
  }
}
