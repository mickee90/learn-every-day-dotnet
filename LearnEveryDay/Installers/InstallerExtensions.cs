using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearnEveryDay.Installers
{
  public static class InstallerExtensions
  {
    // Loop all classes which has implemented the IInstaller interface, but exclude interface and abstract classes
    public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
    {
      var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
          typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

      installers.ForEach(installer => installer.InstallServices(services, configuration));
    }
  }
}