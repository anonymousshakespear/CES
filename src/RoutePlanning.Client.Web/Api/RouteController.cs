using System.Net.Http.Headers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutePlanning.Application.Locations.Commands.BookSegment;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;
using RoutePlanning.Application.Locations.Commands.GetSegment;
using RoutePlanning.Client.Web.Authorization;
using RoutePlanning.Client.Web.Shared;

namespace RoutePlanning.Client.Web.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize(nameof(TokenRequirement))]
public sealed class RoutesController : ControllerBase
{
    private readonly IMediator _mediator;
 
    public RoutesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    public async Task<string> HelloWorld()
    {
        try
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await httpClient
                    .GetAsync(
                        "http://www.google.com")
                )
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return await Task.FromResult("Hello World!").ConfigureAwait(false);
        }
        catch
        {
            throw new Exception("a");
        }
    }

    [HttpPost("[action]")]
    public async Task<string> getSegmentFromOceania(GetSegmentRequestDto getSegmentRequestDto)
    {
        try
        {
            var myContent = JsonConvert.SerializeObject(getSegmentRequestDto);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                using (var response = await httpClient
                    .PostAsync(
                        "https://wa-eit-eit1.azurewebsites.net/api/GetSegment",
                        byteContent
                        )
                )
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Searching unsuccessful");
                    }

                    var finalData = await response.Content.ReadAsStringAsync();

                    var _dataResponse = JToken.Parse(JsonConvert.SerializeObject(finalData));

                    var userResult = new GetSegmentResponseDto()
                    {
                        cost = Convert.ToDouble(_dataResponse?.Value<double>("Cost").ToString()),
                        time = Convert.ToDouble(_dataResponse?.Value<int>("Time").ToString()),
                    };
                }
            }

            return await Task.FromResult("Hello World!").ConfigureAwait(false);
        }
        catch
        {
            throw new Exception("a");
        }
    }    
    
    [HttpPost("[action]")]
    public async Task<string> bookSegmentFromOceania(BookSegmentRequestDto bookSegmentRequestDto)
    {
        try
        {
            var myContent = JsonConvert.SerializeObject(bookSegmentRequestDto);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Issuer", "EIT");
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                using (var response = await httpClient
                    .PostAsync(
                        "https://wa-eit-eit1.azurewebsites.net/api/BookSegment",
                        byteContent
                        )
                )
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Booking unsuccessful");
                    }

                    var finalData = await response.Content.ReadAsStringAsync();
                    var _dataResponse = JToken.Parse(JsonConvert.SerializeObject(finalData));
                    if (_dataResponse != null)
                    {
                        confirmationID = _dataResponse.Value<string>("ConfirmationID") ?? "0",
                        cost = Convert.ToDouble(_dataResponse?.Value<double>("Cost").ToString()),
                        time = Convert.ToDouble(_dataResponse?.Value<int>("Time").ToString()),
                    };

                        var userResult = new BookSegmentResponseDto()
                        {
                            confirmationID = _dataResponse["ConfirmationID"] != null ? _dataResponse["ConfirmationID"].ToString() : "",
                            cost = Convert.ToDouble(_dataResponse["Cost"].ToString()),
                            time = Convert.ToDouble(_dataResponse["Time"].ToString()),
                        };
                    }
                }
            }

            return await Task.FromResult("Hello World!").ConfigureAwait(false);
        }
        catch
        {
            throw new Exception("a");
        }
    }

    [HttpPost("[action]")]
    public async Task<string> deleteSegmentFromOceania(string confirmationId)
    {
        try
        {
            var myContent = JsonConvert.SerializeObject(new DeleteSegmentRequestDto(confirmationId));
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Issuer", "EIT");
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                using (var response = await httpClient
                    .PostAsync(
                        "https://wa-eit-eit1.azurewebsites.net/api/DeleteSegment",
                        byteContent
                        )
                )
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Deleting unsuccessful");
                    }
                }
            }

            return await Task.FromResult("Hello World!").ConfigureAwait(false);
        }
        catch
        {
            throw new Exception("a");
        }
    }

    [HttpPost("[action]")]
    public async Task<string> AddTwoWayConnection(CreateTwoWayConnectionCommand command)
    {
        try
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await httpClient
                    .GetAsync(
                        "http://www.google.com")
                )
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            await _mediator.Send(command);
            return await Task.FromResult("Hello World!").ConfigureAwait(false);
        }
        catch
        {
            return await Task.FromResult("Hello Worldwdwdwdwdwd!").ConfigureAwait(false);
        }
    }

    [HttpPost("[action]")]
    public async Task<SegmentDto> GetSegment(GetSegmentCommand command)
    {
        var _ = command;
        var tempObject = new SegmentDto(50, 8);
        return await Task.FromResult(tempObject);
    }

    [HttpPost("[action]")]
    public async Task<ConfirmationDto> BookSegment(BookSegmentCommand command)
    {
        var _ = command;
        var tempObject = new ConfirmationDto("abc", 50, 8);
        return await Task.FromResult(tempObject);
    }

    [HttpDelete("[action]")]
    public void DeleteBooking(BookSegmentCommand command)
    {
        var _ = command;
    }
}
