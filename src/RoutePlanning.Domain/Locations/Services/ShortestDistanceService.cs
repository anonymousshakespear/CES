﻿using Microsoft.EntityFrameworkCore;
using RoutePlanning.Domain.Locations.Services.Interfaces;

namespace RoutePlanning.Domain.Locations.Services;

public sealed class ShortestDistanceService : IShortestDistanceService
{
    private readonly IQueryable<Location> _locations;

    public ShortestDistanceService(IQueryable<Location> locations)
    {
        _locations = locations;
    }

    public int CalculateShortestDistance(Location source, Location target)
    {
       // var locations = _locations.Include(l => l.Connections).ThenInclude(c => c.Destination);

        var path = CalculateShortestPath( source, target);

        return path.Sum(c => c.Distance);
    }

    public IEnumerable<Connection> CalculateShortestPath(Location source, Location target)
    {
        var locations = _locations.Include(l => l.Connections).ThenInclude(c => c.Destination);

        return CalculateShortestPath(locations, source, target);
    }

    /// <summary>
    /// An implementation of the Dijkstra's shortest path algorithm
    /// </summary>
    private static IEnumerable<Connection> CalculateShortestPath(IEnumerable<Location> locations, Location start, Location end)
    {
        var shortestConnections = CalculateShortestConnections(locations, start, end);

        var path = ConstructShortestPath(start, end, shortestConnections);

        return path;
    }


    public static (int, int) CalculateTimePrice(IEnumerable<Connection> path)
    {
        return (path.Sum(c => c.Time), path.Sum(c => c.Price));
    }

    /// <summary>
    /// An implementation of the Dijkstra's algorithm that computes the shortest connections to all locations until the end location is reached
    /// </summary>
    private static Dictionary<Location, (Connection? SourceConnection, int Distance)> CalculateShortestConnections(IEnumerable<Location> locations, Location start, Location end)
    {
        var shortestConnections = new Dictionary<Location, (Connection? SourceConnection, int EdgeWeight)>();
        var unvisitedLocations = locations.ToHashSet();

        foreach (var location in unvisitedLocations)
        {
            shortestConnections[location] = (SourceConnection: null, EdgeWeight: int.MaxValue);
        }

        shortestConnections[start] = (SourceConnection: null, EdgeWeight: 0);

        while (unvisitedLocations.Count > 0)
        {
            var location = unvisitedLocations.OrderBy(location => shortestConnections[location].EdgeWeight).First();

            if (location == end)
            {
                break;
            }

            foreach (var connection in location.Connections)
            {
                UpdateShortestConnections(shortestConnections, location, connection);
            }

            unvisitedLocations.Remove(location);
        }

        return shortestConnections;
    }

    private static void UpdateShortestConnections(Dictionary<Location, (Connection? SourceConnection, int Distance)> shortestConnections, Location location, Connection connection)
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
    private static IEnumerable<Connection> ConstructShortestPath(Location start, Location end, Dictionary<Location, (Connection? SourceConnection, int Distance)> sourceConnections)
    {
        var path = new List<Connection>();
        var location = end;

        while (location != start)
        {
            var shortestConnection = sourceConnections[location].SourceConnection!;
            path.Add(shortestConnection);
            location = shortestConnection.Source;
        }

        path.Reverse();

        return path;
    }
}
