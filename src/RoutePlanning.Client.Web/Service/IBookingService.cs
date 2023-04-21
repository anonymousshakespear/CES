using RoutePlanning.Client.Web.DTO;

namespace RoutePlanning.Client.Web.Service;
public interface IBookingService
{
    Task<BookingDto> Add(string ProductCategory, string User, string StartingCity, string DestinationCity, int Height, int Weight, int Depth, int Length, string Remark, string ReceiverInformation, double Cost, string BookingDate, string Status, int packageId);
}
