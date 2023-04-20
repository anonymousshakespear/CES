namespace RoutePlanning.Domain.Locations.Models;
public class City
{
    public string name { get; set; }
    public bool status { get; set; }
    public DateTime dataRowVersion { get; set; }
   
    public City(string name, bool status)
    {
        this.name = name;
        this.status = status;
    }
}
