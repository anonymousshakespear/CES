
using RoutePlanning.Domain.Locations.Models;

namespace RoutePlanning.Domain.Locations.Repository;
public class UserProfileRepository
{
    public void createUserProfile(UserProfile userProfile)
    {
    }

    public void updateUserProfile(UserProfile userProfile) {}

    public void deleteUserProfile(UserProfile userProfile) {}

    public UserProfile getUserProfileByID(int id) { return new UserProfile(); }
}
