using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Application.Locations.Queries.City;

namespace RoutePlanning.Application.Locations.Queries.SelectableLocationList;

public sealed class SelectableCityQueryhandler : IQueryHandler<SelectableCityQuery, IReadOnlyList<SelectableCity>>
{
    private readonly IQueryable<Domain.Locations.Models.City> _cities;

    public SelectableCityQueryhandler(IQueryable<Domain.Locations.Models.City> cities)
    {
        _cities = cities;
    }

    public async Task<IReadOnlyList<SelectableCity>> Handle(SelectableLocationListQuery _, CancellationToken cancellationToken)
    {
        return await _cities
            .Select(l => new SelectableCity(l.Id, l.name, l.status, l.dataRowVersion))
            .ToListAsync(cancellationToken);
    }

    public Task<IReadOnlyList<SelectableCity>> Handle(SelectableCityQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}
