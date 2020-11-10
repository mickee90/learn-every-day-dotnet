using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LearnEveryDay.Dtos.User;
using LearnEveryDay.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LearnEveryDay.Data.Repository
{
  public class UserRepository : IUserRepository
  {
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly AppConfiguration _appConfig;

    public UserRepository(UserManager<User> userManager, AppDbContext context, AppConfiguration appConfig)
    {
      _userManager = userManager;
      _context = context;
      _appConfig = appConfig;
    }

    public IEnumerable<User> GetAll()
    {
      return _context.Users;
    }

    public async Task<UserReadDto> AuthenticateAsync(AuthenticateRequestDto userDto)
    {
      var user = await _userManager.FindByEmailAsync(userDto.UserName);

      if (user != null && await _userManager.CheckPasswordAsync(user, userDto.Password))
      {
        var token = generateJwtToken(user);

        return new UserReadDto(user, token);
      }
      else
      {
        throw new KeyNotFoundException();
      }
    }

    public async Task<UserReadDto> RegisterAsync(AuthenticateRequestDto userDto)
    {
      var user = await _userManager.FindByEmailAsync(userDto.UserName);

      if (user != null && await _userManager.CheckPasswordAsync(user, userDto.Password))
      {
        var token = generateJwtToken(user);

        return new UserReadDto(user, token);
      }
      else
      {
        return null;
      }
    }

    // Generate a new Jwt token with proper claims
    private string generateJwtToken(User user)
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

      return tokenHandler.WriteToken(token);
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
      return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync() >= 0);
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
      if (user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      _context.Users.Update(user);

      return await SaveChangesAsync();
    }
  }
}
