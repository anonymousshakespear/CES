using Netcompany.Net.Cqs.Commands;

namespace RoutePlanning.Application.Locations.Commands.External;

public sealed record GetSegmentTelstarCommand(string fromCity, string toCity, int weight, int height, int width, int depth, string type) : ICommand;
