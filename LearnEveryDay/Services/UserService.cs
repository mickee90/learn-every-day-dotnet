using System;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Data.Entities;
using LearnEveryDay.Domain;
using LearnEveryDay.Repositories;

namespace LearnEveryDay.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResult> UpdatePasswordAsync(Guid userId, UpdatePasswordRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
      
            if (string.IsNullOrEmpty(request.Password))
            {
                return new UserResult
                {
                    Errors = new[] {"The password must not be empty."}
                };
            }
      
            if (request.Password != request.ConfirmPassword)
            {
                return new UserResult
                {
                    Errors = new[] {"The passwords do not match."}
                };
            }

            var user = await _repository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return new UserResult
                {
                    Errors = new[] {"The user could not be found"}
                };
            }

            var response = await _repository.UpdatePasswordAsync(user, request.Password);
            
            if (!response.Success)
            {
                return new UserResult { Errors = response.Errors };
            }
      
            return new UserResult
            {
                User = user,
                Success = true,
            };
        }
        
        public async Task<UserResult> UpdateUserAsync(User user, UpdateUserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (user == null)
            {
                return new UserResult
                {
                    Errors = new[] { "The user could not be found" }
                };
            }
      
            user.UserName = request.UserName;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Address = request.Address;
            user.ZipCode = request.ZipCode;
            user.City = request.City;
            user.Email = request.Email;
            user.Phone = request.Phone;
      
            var result = await _repository.UpdateUserAsync(user);
      
            if (!result.Success)
            {
                return new UserResult { Errors = result.Errors };
            }
      
            return new UserResult
            {
                User = user,
                Success = true,
            };
        }
    }
}