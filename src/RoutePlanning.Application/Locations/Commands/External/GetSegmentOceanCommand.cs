using Netcompany.Net.Cqs.Commands;

namespace RoutePlanning.Application.Locations.Commands.External;

public sealed record GetSegmentOceanCommand(string Start, string End, int Weight, int Height, int Width, int Depth, string Type) : ICommand;
