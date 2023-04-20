using Netcompany.Net.Cqs.Commands;

namespace RoutePlanning.Application.Locations.Commands.DeleteBooking;

public sealed record DeleteBookingCommand(string ConfirmationId) : ICommand;
