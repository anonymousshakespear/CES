
namespace RoutePlanning.Domain.Locations.Models;
public class UserProfile
{
    private int ID;
    private string name;
    private string userName;
    private string password;
    private bool isAdmin;
    private DateTime rowVersion;

    public UserProfile(int ID, string name, string userName, string password, bool isAdmin, DateTime rowVersion)
    {
        this.ID = ID;
        this.name = name;
        this.userName = userName;
        this.password = password;
        this.rowVersion = rowVersion;
        this.isAdmin = isAdmin;
    }

    public UserProfile () {}

    public int getID() { return ID; }
    public string getName() { return name; }
    public string getUserName() { return userName; }
    public string getPassword() { return password; }
    public bool getIsAdmin() { return isAdmin; }
    public void setIsAdmin(bool isAdmin) {  this.isAdmin = isAdmin; }
    public void setID(int ID) { this.ID = ID;}

    public void setUserName(string userName)
    {
        this.userName = userName;
    }

    public void setPassword(string password) { this.password = password; }
    public void setName(string name) { this.name = name;}
}
