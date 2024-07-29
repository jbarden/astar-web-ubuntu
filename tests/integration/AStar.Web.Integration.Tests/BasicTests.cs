using Microsoft.AspNetCore.Mvc.Testing;

namespace AStar.Web.Integration.Tests;

public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public BasicTests(WebApplicationFactory<Program> factory) => this.factory = factory;

    [Theory]
    [InlineData("/")]
    [InlineData("/admin")]
    [InlineData("/counter")]
    [InlineData("/weather")]
    public async Task GetEndpointsReturnSuccessAndCorrectContentType(string url)
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType?.ToString().Should().Be("text/html; charset=utf-8");
    }
}