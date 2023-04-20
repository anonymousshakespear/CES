using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Source} --{Distance} -- {price} --> {Destination}")]
public sealed class Connection : Entity<Connection>
{
    public Connection(Location source, Location destination, Distance distance)
    {
        Source = source;
        Destination = destination;
        Distance = distance;
        Price = 0;
    }    
    public Connection(Location source, Location destination, Distance distance, double price)
    {
        Source = source;
        Destination = destination;
        Distance = distance;
        Price = price;
    }

    private Connection()
    {
        Source = null!;
        Destination = null!;
        Distance = null!;
        Price = 0;
    }

    public Location Source { get; private set; }

    public Location Destination { get; private set; }

    public Distance Distance { get; private set; }
    public double Price { get; private set; }
}
