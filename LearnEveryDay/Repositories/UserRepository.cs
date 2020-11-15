using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Data;
using LearnEveryDay.Domain;
using LearnEveryDay.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LearnEveryDay.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly AppConfiguration _appConfig;
    private readonly IMapper _mapper;

    public UserRepository(UserManager<User> userManager, AppDbContext context, AppConfiguration appConfig, IMapper mapper)
    {
      _userManager = userManager;
      _context = context;
      _appConfig = appConfig;
      _mapper = mapper;
    }

    public async Task<AuthenticationResult> AuthenticateAsync(UserLoginRequest authRequest)
    {
      var user = await _userManager.FindByNameAsync(authRequest.UserName);

      if (user == null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User do not exist" }
        };
      }

      var validatedPassword = await _userManager.CheckPasswordAsync(user, authRequest.Password);
      if (!validatedPassword)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "Username and password combination do not exist" }
        };
      }

      return generateJwtToken(user);
    }

    public async Task<AuthenticationResult> RegisterAsync(UserRegisterRequest registerRequest)
    {
      var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);

      if (existingUser != null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User with this email address already exists" }
        };
      }

      var user = _mapper.Map<User>(registerRequest);

      if (registerRequest.Email != null)
      {
        // user.Id = Guid.NewGuid();
        user.UserName = registerRequest.Email;
      }

      var createdUser = await _userManager.CreateAsync(user, registerRequest.Password);

      if (!createdUser.Succeeded)
      {
        return new AuthenticationResult
        {
          Errors = createdUser.Errors.Select(x => x.Description)
        };
      }

      await _userManager.AddToRoleAsync(user, "User");

      return generateJwtToken(user);
    }

    // Generate a new Jwt token with proper claims
    private AuthenticationResult generateJwtToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appConfig.JwtSecret);

      var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id.ToString())
            };

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return new AuthenticationResult
      {
        User = user,
        Success = true,
        Token = tokenHandler.WriteToken(token),
      };
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
      return await _userManager.FindByIdAsync(userId.ToString());
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync() >= 0);
    }

    public async Task<UserResult> UpdatePasswordAsync(User user, string password)
    {
      // Use Generate Token + ResetPassword here since "UpdatePassword" already requires the user to be logged in
      // And we don't want them to have to enter a 'current password' field in this scenario
      var token = await _userManager.GeneratePasswordResetTokenAsync(user);

      var result = await _userManager.ResetPasswordAsync(user, token, password);
      
      if (!result.Succeeded)
      {
        return new UserResult
        {
          Errors = result.Errors.Select(x => x.Description)
        };
      }
      
      return new UserResult
      {
        User = user,
        Success = true,
      };

    }

    public async Task<UserResult> UpdateUserAsync(User user)
    {
      var result = await _userManager.UpdateAsync(user);
      
      if (!result.Succeeded)
      {
        return new UserResult
        {
          Errors = result.Errors.Select(x => x.Description)
        };
      }
      
      return new UserResult
      {
        User = user,
        Success = true,
      };
    }
  }
}
