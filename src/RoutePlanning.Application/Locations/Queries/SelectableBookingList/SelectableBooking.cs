namespace RoutePlanning.Application.Locations.Queries.City;

public sealed record SelectableBooking(Domain.Locations.Models.Booking bookingID, int packageID, string productCategory, 
    Guid userID, string startingCity, string destinationCity, float height, float weight, float depth, float length, 
    string remark, string receiverInformation, float cost, DateTime bookingDate, string status);
