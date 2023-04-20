using Netcompany.Net.Cqs.Commands;

namespace RoutePlanning.Application.Locations.Commands.BookSegment;

public sealed record BookSegmentCommand(string Start, string End, int Weight, int Height, int Width, int Depth, string Type, string Time, string Packageid) : ICommand;
