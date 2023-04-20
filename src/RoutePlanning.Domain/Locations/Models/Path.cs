
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations.Models;
public class Path : AggregateRoot<Path>
{
    public int startCityID { get; set; }
    public int destinationCityID { get; set; }
    public string transportKind { get; set; }
    public float cost { get; set; }
    public int segments { get; set; }
    public DateTime rowVersion { get; set; }

    public Path(int startCityID, int destinationCityID, string transportKind, float cost, int segments,
        DateTime rowVersion)
    {
        this.startCityID = startCityID;
        this.destinationCityID = destinationCityID;
        this.transportKind = transportKind;
        this.cost = cost;
        this.segments = segments;
        this.rowVersion = rowVersion;
    }
}
