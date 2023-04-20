
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations.Models;
public class UserProfile : AggregateRoot<UserProfile>
{
    public string Name { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public bool isAdmin { get; set; }

    public UserProfile(string name, string userName, string password, bool admin)
    {
        Name = name;
        UserName = userName;
        Password = password;
        isAdmin = admin;
    }
}
