using System.Data;

namespace RoutePlanning.Domain.Locations.Models;
public class City
{
    private Int32 ID;
    private String name;
    private bool status;
    private DataRowVersion DateTime;
    public City(){}
    public City(Int32 ID, String name, bool status)
    {
        this.ID = ID;
        this.name = name;
        this.status = status;
    }

    public Int32 getID () { return ID; }
    public String getName () { return name; }
    public bool getStatus () { return status; }

    public DataRowVersion getDate() {return DateTime;}
    public void setDate(DataRowVersion date) { DateTime = date;}
    public void setID(Int32 ID) { this.ID = ID; }
    public void setName(String name) { this.name = name;}
    public void setStatus (bool status) {  this.status = status;}
}
