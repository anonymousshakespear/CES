using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoutePlanning.Domain.Locations.Models;

namespace RoutePlanning.Infrastructure.Database.Locations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StartingCity);

        builder.Property(x => x.DestinationCity);

        builder.Property(x => x.UserID);

        builder.Property(x => x.PackageID);

        builder.Property(x => x.ProductCategory);

        builder.Property(x => x.Height);

        builder.Property(x => x.Weight);

        builder.Property(x => x.Depth);

        builder.Property(x => x.Length);

        builder.Property(x => x.Remark);

        builder.Property(x => x.ReceiverInformation);

        builder.Property(x => x.Cost);

        builder.Property(x => x.BookingDate);

        builder.Property(x => x.Status);
    }
}
