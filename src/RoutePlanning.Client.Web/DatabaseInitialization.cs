using System;
using System.Collections;
using Netcompany.Net.UnitOfWork;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Locations.Models;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Database;

namespace RoutePlanning.Client.Web;

public static class DatabaseInitialization
{
    public static async Task SeedDatabase(WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<RoutePlanningDatabaseContext>();
        await context.Database.EnsureCreatedAsync();

        var unitOfWorkManager = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        await using (var unitOfWork = unitOfWorkManager.Initiate())
        {
            //await SeedUsers(context);
            //await SeedLocationsAndRoutes(context);

            await SeedUserProfile(context);

            unitOfWork.Commit();
        }
    }

    private static async Task SeedLocationsAndRoutes(RoutePlanningDatabaseContext context)
    {
        
        
        var Tanger = new Location("Tanger");
        await context.AddAsync(Tanger);

        var copenhagen = new Location("Copenhagen");
        await context.AddAsync(copenhagen);

        var paris = new Location("Paris");
        await context.AddAsync(paris);

        var warsaw = new Location("Warsaw");
        await context.AddAsync(warsaw);

        



        //change the var name to distanceOrPrice / wieght´- it will be all right then :)
        CreateTwoWayConnection(Tanger, warsaw, 573);
        CreateTwoWayConnection(Tanger, copenhagen, 763);
        CreateTwoWayConnection(Tanger, paris, 1054);
        CreateTwoWayConnection(copenhagen, paris, 1362);
    }

    private static async Task SeedUserProfile(RoutePlanningDatabaseContext context)
    {
        var alice = new UserProfile("test", "abc", "123456789", false);
        await context.AddAsync(alice);
    }

    private static async Task SeedUsers(RoutePlanningDatabaseContext context)
    {
        var alice = new User("alice", User.ComputePasswordHash("alice123!"));
        await context.AddAsync(alice);

        var bob = new User("bob", User.ComputePasswordHash("!CapableStudentCries25"));
        await context.AddAsync(bob);
    }

    private static void CreateTwoWayConnection(Location locationA, Location locationB, int distance)
    {
        locationA.AddConnection(locationB, distance);
        locationB.AddConnection(locationA, distance);
    }
}
