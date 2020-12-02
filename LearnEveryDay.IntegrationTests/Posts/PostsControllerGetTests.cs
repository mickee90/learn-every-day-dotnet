using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using LearnEveryDay.Contracts.v1.Responses;
using Xunit;

namespace LearnEveryDay.IntegrationTests
{
    public class PostsControllerGetTests : IntegrationTest
    {
        [Fact]
        public async Task Index_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            await AuthenticateAsync();

            var response = await TestClient.GetAsync("/api/v1/posts");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<PostsResponse>()).Data.Should().BeEmpty();
        }
    }
}