using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutePlanning.Application.Locations.Commands.BookSegment;
using RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;
using RoutePlanning.Application.Locations.Commands.GetSegment;
using RoutePlanning.Client.Web.Authorization;

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
    public Task<string> HelloWorld()
    {
        return Task.FromResult("Hello World!");
    }

    [HttpPost("[action]")]
    public async Task AddTwoWayConnection(CreateTwoWayConnectionCommand command)
    {
        await _mediator.Send(command);
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
