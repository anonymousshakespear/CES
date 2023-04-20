using System.ComponentModel.DataAnnotations.Schema;
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
    }

    public Location Source { get; private set; }

    public Location Destination { get; private set; }

    public Distance Distance { get; private set; }
    
}
public sealed class WheightedConnection : Entity<Connection>
{
    public WheightedConnection(Location source, Location destination, Distance distance, Time time)
    {
        Source = source;
        Destination = destination;
        Distance = distance;
        Time = time;
        Price = price;
        EdgeWheight = edgeWheight;

    }

    private WheightedConnection()
    {
        Source = null!;
        Destination = null!;
        Distance = null!;
        Source = null!;
        Destination = null!;
        Distance = null!;
    }

    public Location Source { get; private set; }

    public Location Destination { get; private set; }

    public Distance Distance { get; private set; }

    public Distance Time { get; private set; }
    public Distance Price { get; private set; }
    public Distance EdgeWheight { get; private set; }

}
