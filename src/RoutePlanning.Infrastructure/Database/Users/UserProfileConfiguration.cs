using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoutePlanning.Domain.Locations.Models;

namespace RoutePlanning.Infrastructure.Database.Users;

public sealed class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName);
        builder.Property(x => x.Password);
        builder.Property(x => x.IsAdmin);
    }
}
