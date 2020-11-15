
using LearnEveryDay.Data;
using LearnEveryDay.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearnEveryDay.Installers
{
    public class DbInstaller : IInstaller
    {
        // Add all db related services
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
        }
    }
}