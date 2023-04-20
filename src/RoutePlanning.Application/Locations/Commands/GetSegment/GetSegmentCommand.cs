using Netcompany.Net.Cqs.Commands;

namespace RoutePlanning.Application.Locations.Commands.GetSegment;

public sealed record GetSegmentCommand(string Start, string End, int Weight, int Height, int Width, int Depth, string Type) : ICommand;
