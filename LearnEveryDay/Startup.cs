using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LearnEveryDay.Installers;
using LearnEveryDay.Options;

namespace LearnEveryDay
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
  
    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      // Single line to import all services located in the Installers-folder by the InstallerExtensions.cs
      services.InstallServicesInAssembly(Configuration);
      services.AddAutoMapper(typeof(Startup));

      // services.AddMemoryCache();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

      var swaggerOptions = new SwaggerOptions();
      Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
      
      app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

      app.UseSwaggerUI(option =>
      {
        option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
      });

      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
