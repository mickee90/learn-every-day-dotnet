using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using LearnEveryDay.Dtos.User;
using LearnEveryDay.Models;
using Microsoft.IdentityModel.Tokens;

namespace LearnEveryDay.Data.Repository
{
  public class UserRepository : IUserRepository
  {
    private readonly AppDbContext _context;
    private readonly AppConfiguration _appConfig;

    public UserRepository(AppDbContext context, AppConfiguration appConfig)
    {
      _context = context;
      _appConfig = appConfig;
    }

    private List<User> Users = new List<User>
    {
        new User {
            Id = Guid.Parse("D059FF23-A35C-4C7C-9B5E-C1C2CD01C173"),
            FirstName = "Test",
            LastName = "User",
            UserName = "test",
            Password = "test" }
    };

    public IEnumerable<User> GetAll()
    {
      return Users;
    }

    public UserReadDto Authenticate(AuthenticateRequestDto model)
    {
      var user = Users.SingleOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

      if (user == null) return null;

      var token = generateJwtToken(user);

      return new UserReadDto(user, token);
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

    public void CreateUser(User User)
    {
      if (User == null)
      {
        throw new ArgumentNullException(nameof(User));
      }

      _context.Users.Add(User);
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
