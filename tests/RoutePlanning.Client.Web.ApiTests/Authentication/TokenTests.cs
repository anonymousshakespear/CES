using System.Net.Mime;
using System.Text;
using Azure;
using Netcompany.Net.Testing.Api;
using Newtonsoft.Json;
using RoutePlanning.Application.Locations.Commands.External;
using RoutePlanning.Client.Web.Api;

namespace RoutePlanning.Client.Web.ApiTests.Authentication;

public class TokenTests : IClassFixture<RoutePlanningApplicationFactory>
{
    private readonly RoutePlanningApplicationFactory _factory;
    private readonly HttpClient _client;

    public TokenTests(RoutePlanningApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.HttpClient;
    }

    [Fact]
    public async void ShouldGetHelloWorld()
    {
        // Arrange
        var url = _factory.GetRoute<Program, RoutesController>(x => x.HelloWorld);

        // Act
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Token", "TheSecretApiToken");

        var response = await _client.SendAsync(request);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello World!", content);
    }
    [Fact]
    public async void ShouldGetSegment()
    {
        // Arrange
        var segment = new GetSegmentOceanCommand("Luanda", "Kabalo", 5, 5, 1, 30, "Standard");



        // Assert
        var result = string.Empty;
        using (var httpClient = new HttpClient())
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://wa-oa-dk1.azurewebsites.net/api/GetSegment"),
                Content = new StringContent(JsonConvert.SerializeObject(segment), Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            httpClient.DefaultRequestHeaders.Add("ISSUER", "EIT");

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        
        var returnedcontent =  await Task.FromResult(result).ConfigureAwait(false);
        
        Assert.Equal("Hello World!", returnedcontent);




    }


}
