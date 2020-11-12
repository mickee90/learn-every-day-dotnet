﻿using System;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Domain;
using LearnEveryDay.Models;

namespace LearnEveryDay.Data.Repository
{
  public interface IUserRepository
  {
    Task<bool> SaveChangesAsync();

    Task<User> GetUserByIdAsync(Guid id);
    Task<bool> UpdateUserAsync(User User);
    Task<AuthenticationResult> AuthenticateAsync(UserLoginRequest authRequest);
    Task<AuthenticationResult> RegisterAsync(UserRegisterRequest registerRequest);
  }
}
