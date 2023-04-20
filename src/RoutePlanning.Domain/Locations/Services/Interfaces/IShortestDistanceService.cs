using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain.Locations.Services.Interfaces;

public interface IShortestDistanceService : IDomainService
{
    int CalculateShortestDistance(Location source, Location target);


}
