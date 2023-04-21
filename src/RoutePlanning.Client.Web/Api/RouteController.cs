using System.Net.Http.Headers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoutePlanning.Application.Locations.Commands.BookSegment;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;
using RoutePlanning.Application.Locations.Commands.GetSegment;
using RoutePlanning.Client.Web.Shared;
using RoutePlanning.Domain.Locations.Services;
using RoutePlanning.Application.Locations.Commands.FindPath;
using System.Net;
using RoutePlanning.Application.Locations.Commands.External;
using System.Net.Mime;
using System.Text;
using RoutePlanning.Application.Locations.Commands.CreateUser;
using RoutePlanning.Client.Web.DTO;
using RoutePlanning.Client.Web.Service;

namespace RoutePlanning.Client.Web.Api;

[Route("api/[controller]")]
[ApiController]
public sealed class RoutesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBookingService _bookingService;
 
    public RoutesController(IMediator mediator, IBookingService bookingService)
    {
        _mediator = mediator;
        _bookingService = bookingService;
    }

    [HttpGet("hello")]
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
    public async Task<string> GetSegmentFromTelstar(GetSegmentTelstarCommand segment)
    {
        var result = string.Empty;
        using (var httpClient = new HttpClient())
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://wa-tl-dk1.azurewebsites.net/api/GetSegment"),
                Content = new StringContent(JsonConvert.SerializeObject(segment), Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        return await Task.FromResult(result).ConfigureAwait(false);
    }

    [HttpPost("[action]")]
    public async Task<string> GetSegmentFromOcean(GetSegmentOceanCommand segment)
    {
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

        return await Task.FromResult(result).ConfigureAwait(false);
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

                    var userResult = new BookSegmentResponseDto()
                    {
                        confirmationID = _dataResponse.Value<string>("ConfirmationID") ?? "0",
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
    public async Task<SegmentDto> FindShortestPath(FindShortestPathCommand command)
    {
        try
        {
            var (time, price) = RoutingService.FindShortestRoute(command.From, command.To, command.Category, command.Weight);
            var SegmentDto = new SegmentDto(price, time);
            return await Task.FromResult(SegmentDto);
        } catch (Exception ex)
        {
            var _ = ex;
            return await Task.FromResult(new SegmentDto(30, 8));
        }
    }

    [HttpPost("[action]")]
    public async Task<BookingDto> CreateBooking(CreateBookingCommand command)
    {
        var dto = await _bookingService.Add(command.ProductCategory, command.User, command.StartingCity, command.DestinationCity, command.Height, command.Weight, command.Depth, command.Length, command.Remark, command.ReceiverInformation, command.Cost, command.BookingDate, command.Status, 0);
        return await Task.FromResult(dto);
    }

    [HttpPost("[action]")]
    public async Task<SegmentDto> GetSegment(GetSegmentCommand command)
    {
        var (time, price) = RoutingService.CalculateTimeAndCostOfSegmet(command.Start, command.End, string.Empty, command.Weight);
        var SegmentDto = new SegmentDto(price, time);
        return await Task.FromResult(SegmentDto);

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
