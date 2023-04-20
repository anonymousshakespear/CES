using Microsoft.EntityFrameworkCore;

namespace RoutePlanning.Domain.Locations.Services;

public sealed class CheapestDistanceService : ICheapestDistanceService
{
    private readonly IQueryable<Location> _locations;

    public CheapestDistanceService(IQueryable<Location> locations)
    {
        _locations = locations;
    }

    public double CalculateCheapestDistance(Location source, Location target)
    {
        var locations = _locations.Include(l => l.Connections).ThenInclude(c => c.Destination);

        var path = CalculateCheapestPath(locations, source, target);

        return path.Sum(c => c.Price);
    }

    /// <summary>
    /// An implementation of the Dijkstra's shortest path algorithm
    /// </summary>
    private static IEnumerable<Connection> CalculateCheapestPath(IEnumerable<Location> locations, Location start, Location end)
    {
        var shortestConnections = CalculateShortestConnections(locations, start, end);

        var path = ConstructCheapestPath(start, end, shortestConnections);

        return path;
    }

    /// <summary>
    /// An implementation of the Dijkstra's algorithm that computes the shortest connections to all locations until the end location is reached
    /// </summary>
    private static Dictionary<Location, (Connection? SourceConnection, int Price)> CalculateShortestConnections(IEnumerable<Location> locations, Location start, Location end)
    {
        var cheapestConnections = new Dictionary<Location, (Connection? SourceConnection, int Price)>();
        var unvisitedLocations = locations.ToHashSet();

        foreach (var location in unvisitedLocations)
        {
            cheapestConnections[location] = (SourceConnection: null, Price: int.MaxValue);
        }

        cheapestConnections[start] = (SourceConnection: null, Price: 0);

        while (unvisitedLocations.Count > 0)
        {
            var location = unvisitedLocations.OrderBy(location => cheapestConnections[location].Price).First();

            if (location == end)
            {
                break;
            }

            foreach (var connection in location.Connections)
            {
                UpdateCheapestConnections(cheapestConnections, location, connection);
            }

            unvisitedLocations.Remove(location);
        }

        return cheapestConnections;
    }

    private static void UpdateCheapestConnections(Dictionary<Location, (Connection? SourceConnection, int Distance)> shortestConnections, Location location, Connection connection)
    {
        var distance = shortestConnections[location].Distance + connection.Distance;

        if (distance < shortestConnections[connection.Destination].Distance)
        {
            shortestConnections[connection.Destination] = (SourceConnection: connection, Distance: distance);
        }
    }

    /// <summary>
    /// The shortest path is constructed by backtracking the Dijkstra connection data from the end location
    /// </summary>
    private static IEnumerable<Connection> ConstructCheapestPath(Location start, Location end, Dictionary<Location, (Connection? SourceConnection, int Price)> sourceConnections)
    {
        var path = new List<Connection>();
        var location = end;

        while (location != start)
        {
            var cheapestConnection = sourceConnections[location].SourceConnection!;
            path.Add(cheapestConnection);
            location = cheapestConnection.Source;
        }

        path.Reverse();

        return path;
    }
}
