using System;
using System.Collections.Generic;
using LearnEveryDay.Dtos.User;
using LearnEveryDay.Models;

namespace LearnEveryDay.Data.Repository
{
  public interface IUserRepository
  {
    bool SaveChanges();

    User GetUserById(Guid id);
    IEnumerable<User> GetAll();
    void CreateUser(User User);
    void UpdateUser(User User);
    UserReadDto Authenticate(AuthenticateRequestDto model);
  }
}
