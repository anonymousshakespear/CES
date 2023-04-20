using RoutePlanning.Domain.Locations.Services.Interfaces;

namespace RoutePlanning.Domain.Locations.Services;
internal class BookingService : IBookingService
{
    public string makeBooking(IEnumerable<Connection> path, double price, DateTime shippinDateTime, double width, double height, double length,
        ProductCategory productCategory, string packageID)
    {
        throw new NotImplementedException();
    }

    public string makeBookingWithTheThirdParty(Connection route, double price, DateTime shippinDateTime, double width,
        double height, double length, ProductCategory productCategory, string packageID)
    {
        throw new NotImplementedException();
    }

    public bool cancelBooking(string bookingID)
    {
        throw new NotImplementedException();
    }

    public Booking viewBooking(string bookingID)
    {
        throw new NotImplementedException();
    }

    public Booking[] viewAllBookings()
    {
        throw new NotImplementedException();
    }

    public Booking[] viewUserBookings()
    {
        throw new NotImplementedException();
    }
}
