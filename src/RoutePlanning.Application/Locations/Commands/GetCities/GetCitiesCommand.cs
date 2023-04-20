using Netcompany.Net.Cqs.Commands;

namespace RoutePlanning.Application.Locations.Commands.GetCities;

public sealed record GetCitiesCommand(string name, string status) : ICommand;
