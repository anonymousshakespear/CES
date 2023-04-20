using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Source} --{Distance} -- {price} --> {Destination}")]

public sealed class Connection : Entity<Connection>
{
    public Connection(Location source, Location destination, Distance distance, Time time,Price price, EdgeWheight edgeWheight)
    {
     Source = source;
     Destination = destination;
    Distance = distance;
    Time = time;
     Price = price;
            EdgeWheight = edgeWheight;
    }

    public Connection(Location source, Location destination, Distance distance)
    {
        Source = source;
        Destination = destination;
        Distance = distance;
        Time = new Time(distance.Value);
        Price = new Price(distance.Value);
        EdgeWheight = new EdgeWheight(distance.Value);
    }

    private Connection()
    {
        Source = null!;
        Destination = null!;
        Distance = null!;
        Time = null!;
        Price = null!;
        EdgeWheight = null!;
    }

    public Location Source { get; private set; }

    public Location Destination { get; private set; }

    public Distance Distance { get; private set; }

    public Time Time { get; private set; }
    public Price Price { get; private set; }
    public EdgeWheight EdgeWheight { get; private set; }
}
