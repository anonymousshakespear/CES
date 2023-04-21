using Netcompany.Net.Cqs.Commands;

namespace RoutePlanning.Application.Locations.Commands.CreateUser;

public sealed record CreateBookingCommand(string ProductCategory, string User, string StartingCity, string DestinationCity, int Height, int Weight,
    int Depth, int Length, string Remark, string ReceiverInformation, double Cost, string BookingDate, string Status) : ICommand;
