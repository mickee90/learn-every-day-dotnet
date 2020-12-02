using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Requests;
using LearnEveryDay.Contracts.v1.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LearnEveryDay.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;

        protected IntegrationTest()
        {
            /**
             * Create a copy of the LearnEveryDay app
             * Remove the DB context and add an in memory db instead to prevent hitting the develop database
             * Add the services to the service provider and set the app copy to the HttpContext
             */
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContext));
                        services.AddDbContext<DbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });
            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        /**
         * Helper method to quickly register and authenticate a user
         */
        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtToken());
        }

        /**
         * Helper method to create a new post
         */
        protected async Task<ActionResult<PostResponse>> CreatePostAsync(CreatePostRequest request)
        {
            var response = await TestClient.PostAsJsonAsync("api/v1/posts", request);
            
            return await response.Content.ReadAsAsync<ActionResult<PostResponse>>();
        }

        /**
         * Register a new user and return it's token
         */
        private async Task<string> GetJwtToken()
        {
            var response = await TestClient.PostAsJsonAsync("api/v1/auth/register", new UserRegisterRequest
            {
                Email = "text@integration.com",
                Password = "Password123!"
            });

            var registrationResponse = await response.Content.ReadAsAsync<UserResponse>();
            return registrationResponse.Token;
        }
        
        /**
         * Creates a new db scope for each test. Makes sure to delete it after the test is done
         * @todo check if this is true.
         */
        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DbContext>();
            
            context.Database.EnsureDeleted();
        }
    }
}