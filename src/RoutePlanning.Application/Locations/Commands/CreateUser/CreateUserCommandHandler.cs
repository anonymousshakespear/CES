using Netcompany.Net.Cqs.Commands;
using Netcompany.Net.DomainDrivenDesign.Services;
using RoutePlanning.Application.Locations.Commands.CreateUser;
using RoutePlanning.Domain.Locations.Models;

namespace RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IRepository<UserProfile> _userProfile;

    public CreateUserCommandHandler(IRepository<UserProfile> userProfile)
    {
        _userProfile = userProfile;
    }

    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userProfile = new UserProfile(command.Name, command.UserName, command.Password, 0);
        await _userProfile.Add(userProfile, cancellationToken);
    }
}
