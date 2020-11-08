using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnEveryDay.Dtos.User;
using LearnEveryDay.Models;

namespace LearnEveryDay.Data.Repository
{
  public interface IUserRepository
  {
    bool SaveChanges();

    User GetUserById(Guid id);
    IEnumerable<User> GetAll();
    void UpdateUser(User User);
    Task<UserReadDto> Authenticate(AuthenticateRequestDto userDto);
  }
}
