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

    public async Task<UserReadDto> Authenticate(AuthenticateRequestDto userDto)
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

    public async Task<UserReadDto> Register(AuthenticateRequestDto userDto)
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

    private string generateJwtToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appConfig.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }

    public User GetUserById(Guid id)
    {
      return _context.Users.FirstOrDefault(user => user.Id == id);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }

    public void UpdateUser(User User)
    {
      if (User == null)
      {
        throw new ArgumentNullException(nameof(User));
      }

      _context.Users.Update(User);
    }
  }
}
