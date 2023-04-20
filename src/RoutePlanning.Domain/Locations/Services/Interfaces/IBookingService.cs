using System.ComponentModel;
using System.Net.Http.Headers;
using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain.Locations.Services.Interfaces;

public interface IBookingService
{
    //Return booking number
    string makeBooking(IEnumerable<Connection> path, double price, DateTime shippinDateTime, double width,
        double height, double length, ProductCategory productCategory, string packageID);

    string makeBookingWithTheThirdParty(Connection route, double price, DateTime shippinDateTime, double width,
        double height, double length, ProductCategory productCategory, string packageID);

    bool cancelBooking(string bookingID);

    Booking viewBooking(string bookingID);

    Booking[] viewAllBookings();

    Booking[] viewUserBookings();


}

public interface Booking
{
}

public interface ProductCategory
{
}
