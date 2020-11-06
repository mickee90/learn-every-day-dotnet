using System;
using AutoMapper;
using LearnEveryDay.Data;
using LearnEveryDay.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json.Serialization;
using LearnEveryDay.Profiles;
using LearnEveryDay.Helpers;

namespace LearnEveryDay
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      //services.AddDbContext<PostContext>(opt => opt.UseMysqlServer
      //    (Configuration.GetConnectionString("CommanderConnection")));

      services.AddControllers().AddNewtonsoftJson(s =>
      {
        s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      });

      var config = new AppConfiguration();
      config.MysqlUser = Configuration["MysqlUser"];
      config.MysqlPassword = Configuration["MysqlPassword"];
      config.MysqlDatabase = Configuration["MysqlDatabase"];
      config.ConnectionStrings = Configuration["ConnectionStrings:default"];
      config.MockUserId = Guid.Parse(Configuration["MockUserId"]);
      config.Secret = Configuration["JwtSecret"];

      services.AddSingleton<AppConfiguration>(config);
      services.Configure<AppConfiguration>(Configuration.GetSection("AppSettings"));

      //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      // Auto Mapper Configurations
      var mapperConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new PostsProfile());
        mc.AddProfile(new UsersProfile());
      });

      IMapper mapper = mapperConfig.CreateMapper();
      services.AddSingleton(mapper);

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddTransient<IPostRepository, PostRepository>();

      services.AddDbContext<PostContext>(options =>
                  options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
      services.AddDbContext<UserContext>(options =>
                  options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

      services.AddMemoryCache();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

      app.UseMiddleware<JwtMiddleware>();

      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
