using Microsoft.Extensions.Hosting;

namespace RoutePlanning.Client.Web.ApiTests.BackEndTests;
public class RoutingServiceTest
{
    [Fact]

    public void FindShortestRouteTest()
    {
        // Should return the shortest route between two shipment locations
    }
    [Fact]

    public void FindPreferableRouteTest()
    {
        // Should return a route that has the biggest possible part of it made by East India Trading Co
    }

    [Fact]
    public void CalculateRouteCostTest()
    {
        // Should return expected cost of a route
    }
}
