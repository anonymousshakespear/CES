using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain.Locations.Services.Interfaces;

public interface IRoutingService
{
    FindShortestRoute();
    FindCheapestRoute();
    FindPreferableRoute();
}
