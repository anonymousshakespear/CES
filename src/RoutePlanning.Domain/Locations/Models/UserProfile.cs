using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations.Models;
public class UserProfile : AggregateRoot<UserProfile>
{
    public string Name { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public int IsAdmin { get; set; }

    public UserProfile(string name, string userName, string password, int isAdmin)
    {
        Name = name;
        UserName = userName;
        Password = password;
        IsAdmin = isAdmin;
    }
}
