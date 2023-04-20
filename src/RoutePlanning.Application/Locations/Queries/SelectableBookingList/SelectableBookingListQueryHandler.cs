using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Application.Locations.Queries.City;
using RoutePlanning.Application.Locations.Queries.SelectableBookingList;

namespace RoutePlanning.Application.Locations.Queries.SelectableLocationList;

public sealed class SelectableBookingListQueryHandler : IQueryHandler<SelectableBookingListQuery, IReadOnlyList<SelectableBooking>>
{
    private readonly IQueryable<Domain.Locations.Models.Booking> _bookings;

    public SelectableBookingListQueryHandler(IQueryable<Domain.Locations.Models.Booking> bookings)
    {
        _bookings = bookings;
    }

    public async Task<IReadOnlyList<SelectableBooking>> Handle(SelectableBookingListQuery _, CancellationToken cancellationToken)
    {
        return await _bookings
            .Select(l => new SelectableBooking(l, l.PackageID,  l.ProductCategory,
                 l.UserID,  l.StartingCityID,  l.DestinationCityID,  l.Height,  l.Weight,  l.Depth, l.Length,
                 l.Remark,  l.ReceiverInformation, l.Cost, l.BookingDate, l.Status))
            .ToListAsync(cancellationToken);
    }

    public Task<IReadOnlyList<SelectableCity>> Handle(SelectableCityQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
