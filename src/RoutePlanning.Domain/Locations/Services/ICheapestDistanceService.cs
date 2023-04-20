using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain.Locations.Services;

public interface ICheapestDistanceService : IDomainService
{
    double CalculateCheapestDistance(Location source, Location target);
}
