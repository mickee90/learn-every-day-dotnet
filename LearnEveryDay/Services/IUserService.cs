using System;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Data.Entities;
using LearnEveryDay.Domain;

namespace LearnEveryDay.Services
{
    public interface IUserService
    {
        public Task<UserResult> UpdateUserAsync(User user, UpdateUserRequest request);
        public Task<UserResult> UpdatePasswordAsync(Guid userId, UpdatePasswordRequest request);
    }
}