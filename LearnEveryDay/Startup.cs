using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LearnEveryDay.Installers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using SwaggerOptions = LearnEveryDay.Options.SwaggerOptions;

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
      // Single line to import all services located in the Installers-folder by the InstallerExtensions.cs
      services.InstallServicesInAssembly(Configuration);
      services.AddAutoMapper(typeof(Startup));

      // services.AddSwaggerGen(x =>
      // {
      //   x.SwaggerDoc("v1", new OpenApiInfo {Title = "LearnEveryDay API", Version = "v1"});
      //
      //   x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      //   {
      //     Description = "JWT Authentication header using the bearer schema",
      //     Name = "Authentication",
      //     In = ParameterLocation.Header,
      //     Type = SecuritySchemeType.ApiKey,
      //   });
      //
      //   x.AddSecurityRequirement(new OpenApiSecurityRequirement
      //   {
      //     {
      //       new OpenApiSecurityScheme
      //       {
      //         Reference = new OpenApiReference
      //         {
      //           Id = "Bearer",
      //           Type = ReferenceType.SecurityScheme,
      //         }
      //       },
      //       new List<string>()
      //     }
      //   });
      // });
      // services.AddMemoryCache();
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

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

      // services.AddSwaggerGen(c =>
      // {
      // c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
      // });

      // app.UseSwagger();
      //
      // app.UseSwaggerUI(c =>
      // {
      //   c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      // });

      var swaggerOptions = new SwaggerOptions();
      Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
      ;
      app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

      app.UseSwaggerUI(option =>
      {
        option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
      });

      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
