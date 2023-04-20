
namespace RoutePlanning.Domain.Locations.Models;
public class Path
{
    private int ID;
    private int startCityID;
    private int destinationCityID;
    private string transportKind;
    private float cost;
    private int segments;
    private DateTime rowVersion;

    public Path() {}
}
