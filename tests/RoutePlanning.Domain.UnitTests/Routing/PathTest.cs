using System.ComponentModel;
using RoutePlanning.Client.Web.Api;
using RoutePlanning.Client.Web.Shared;
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
        var productCategory = "";
        var weight = 10;
        var (time,price)= RoutingService.FindShortestRoute(cityA, cityB,productCategory,weight);
        Assert.Equal(228,time);
        Assert.Equal(152, price);

    }

    [Fact]
    public void TestPriceChangeCatagory()
    {
        var cityB = "Dakar";
        var cityA = "Cape Town";
        var productCategory = "Live animals, Weapons";
        var weight = 10;
        var (time, price) = RoutingService.FindShortestRoute(cityA, cityB, productCategory, weight);
        Assert.Equal(228, time);
        Assert.Equal((int)(152*1.2*1.25), price);


    }

    [Fact]
    public void TestCancellationCatagory()
    {
        var cityB = "Dakar";
        var cityA = "Cape Town";

        var productCategory = "Cautious parcels";
        var weight = 10;
        var (time, price) = RoutingService.FindShortestRoute(cityA, cityB, productCategory, weight);
        Assert.Equal(-1, time);
        Assert.Equal(-1, price);
    }



    [Fact]
    public void TestPriceChangeTime()
    {
        var cityB = "Dakar";
        var cityA = "Cape Town";
        var productCategory = "";
        var date = "2023-01-15";
        var weight = 10;
        var (time, price) = RoutingService.FindShortestRoute(cityA, cityB, productCategory, weight,date);
        Assert.Equal(228, time);
        Assert.Equal((int)(152), price);

        date = "2023-06-15";
        (time, price) = RoutingService.FindShortestRoute(cityA, cityB, productCategory, weight,date);
        Assert.Equal(228, time);
        Assert.Equal((int)(152.0/8*5), price);
    }

    [Fact]

    public void TestIntegrationOnMap()
    {
        var cityB = "Walvis Bay";
        var cityA = "Cape Town";
        var productCategory = "";
        var weight = 10;
        var (time, price) = RoutingService.FindShortestRoute(cityA, cityB, productCategory, weight);
        Assert.Equal(5, time);
    }
}


public class StubProductCategory : ProductCategory
{

}
