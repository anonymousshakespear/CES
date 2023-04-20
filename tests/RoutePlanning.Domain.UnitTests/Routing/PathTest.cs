using System.ComponentModel;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Locations.Services;
using RoutePlanning.Domain.Locations.Services.Interfaces;

namespace RoutePlanning.Domain.UnitTests.Routing;

public sealed class PathTest
{
    [Fact]
    public void ShortestPathTest()
    {
        // Arrange
        var locationA = new Location("A");
        var locationB = new Location("B");
        var locationC = new Location("C");

        locationA.AddConnection(locationB, 2,4,5,2);
        locationB.AddConnection(locationC, 3, 4, 5, 3);
        locationA.AddConnection(locationC, 6, 4, 5, 6);

        var locations = new List<Location> { locationA, locationB, locationC };

        var shortestDistanceService = new ShortestDistanceService(locations.AsQueryable());

        // Act
        var distance = shortestDistanceService.CalculateShortestDistance(locationA, locationC);

        // Assert
        Assert.Equal(5, distance);
    }
    [Fact]
    public void ShortestPathTestOnMap()
    {
        var cityB = "Dakar";
        var cityA = "Cape Town";
        var productCategory = new StubProductCategory();
        var weight = 10;
        var (time,price)= RoutingService.FindShortestRoute(cityA, cityB,productCategory,weight);
        Assert.Equal(228,time);
    }
}


public class StubProductCategory : ProductCategory
{

}
