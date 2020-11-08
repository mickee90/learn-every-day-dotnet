
using System;
using AutoMapper;
using LearnEveryDay.Data;
using LearnEveryDay.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using LearnEveryDay.Profiles;
using LearnEveryDay.Helpers;
using Microsoft.AspNetCore.Identity;
using LearnEveryDay.Models;
using System.Linq;
using LearnEveryDay.Installers;

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
      config.MockUserId = Guid.Parse(configuration["MockUserId"]);
      config.Secret = configuration["JwtSecret"];

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