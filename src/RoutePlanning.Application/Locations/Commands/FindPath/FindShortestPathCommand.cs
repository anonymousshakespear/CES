using Netcompany.Net.Cqs.Commands;

namespace RoutePlanning.Application.Locations.Commands.FindPath;

public sealed record FindShortestPathCommand(string From, string To, string Category, int Weight) : ICommand;
