namespace RoutePlanning.Application.Locations.Queries.City;

public sealed record SelectableBooking(Domain.Locations.Models.Booking bookingID, int packageID, string productCategory, 
    int userID, int startingCityID, int destinationCityID, float height, float weight, float depth, float length, 
    string remark, string receiverInformation, float cost, DateTime bookingDate, string status);
