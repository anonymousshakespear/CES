
namespace RoutePlanning.Domain.Locations.Services.Interfaces;

public interface IRoutingService
{
    void FindShortestRoute();
    
    void FindCheapestRoute();
    void FindPreferableRoute();
}
