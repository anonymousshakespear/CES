using Netcompany.Net.Cqs.Commands;

namespace RoutePlanning.Application.Locations.Commands.CreateUser;

public sealed record CreateUserCommand(string Name, string UserName, string Password) : ICommand;
