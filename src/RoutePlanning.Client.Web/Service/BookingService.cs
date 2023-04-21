using Netcompany.Net.UnitOfWork;
using RoutePlanning.Client.Web.DTO;
using RoutePlanning.Client.Web.Service;
using RoutePlanning.Domain.Locations.Models;
using RoutePlanning.Infrastructure.Database;

namespace RoutePlanning.Domain.Services;
public class BookingService : IBookingService
{
    private readonly RoutePlanningDatabaseContext _routePlanningDatabaseContext;
    private readonly IUnitOfWorkManager _unitOfWork;

    public BookingService(RoutePlanningDatabaseContext routePlanningDatabaseContext, IUnitOfWorkManager unitOfWork)
    {
        _routePlanningDatabaseContext = routePlanningDatabaseContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<BookingDto> Add(string ProductCategory, string User, string StartingCity, string DestinationCity, int Height, int Weight,
    int Depth, int Length, string Remark, string ReceiverInformation, double Cost, string BookingDate, string Status, int packageId)
    {
        var booking = new Booking(packageId, ProductCategory, Guid.NewGuid(), StartingCity, DestinationCity, Height, Weight, Length, Depth, Remark, ReceiverInformation, (float)Cost, DateTime.Now, Status);
        await _routePlanningDatabaseContext.Database.EnsureCreatedAsync();

        await using (var unitOfWork = _unitOfWork.Initiate())
        {
            await _routePlanningDatabaseContext.AddAsync(booking);

            unitOfWork.Commit();
        }

        return new BookingDto(booking.Id.ToString(), booking.StartingCity, booking.DestinationCity, (int)booking.Cost);
    }
}
