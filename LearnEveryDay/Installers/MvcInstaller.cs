
using AutoMapper;
using LearnEveryDay.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using LearnEveryDay.Profiles;
using Microsoft.AspNetCore.Identity;
using LearnEveryDay.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LearnEveryDay.Installers
{
  public class MvcInstaller : IInstaller
  {

    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
      services.AddControllers().AddNewtonsoftJson(s =>
      {
        s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      });

      var config = new AppConfiguration();
      config.MysqlUser = configuration["MysqlUser"];
      config.MysqlPassword = configuration["MysqlPassword"];
      config.MysqlDatabase = configuration["MysqlDatabase"];
      config.ConnectionStrings = configuration["ConnectionStrings:default"];
      config.JwtSecret = configuration["JwtSecret"];

      services.AddSingleton<AppConfiguration>(config);
      services.Configure<AppConfiguration>(configuration.GetSection("AppSettings"));

      // Auto Mapper Configurations
      var mapperConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new PostsProfile());
        mc.AddProfile(new UsersProfile());
      });

      IMapper mapper = mapperConfig.CreateMapper();
      services.AddSingleton(mapper);

      services.AddScoped<UserManager<User>>();

      // Set up the Jwt authentication
      // Requires app.UseAuthentication() and app.UseAuthorization() in Startup.cs
      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.JwtSecret)),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
      };

      services.AddSingleton(tokenValidationParameters);

      services.AddAuthentication(x =>
          {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(x =>
          {
            x.SaveToken = true;
            x.TokenValidationParameters = tokenValidationParameters;
          });

      services.AddIdentity<User, UserRole>(o =>
      {
        o.Password.RequireDigit = false;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 8;
        o.User.RequireUniqueEmail = true;
      }).AddEntityFrameworkStores<AppDbContext>();
    }
  }

}