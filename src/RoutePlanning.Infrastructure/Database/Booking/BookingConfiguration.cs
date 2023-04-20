using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoutePlanning.Domain.Locations.Models;

namespace RoutePlanning.Infrastructure.Database.Locations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.StartingCity);

        builder.HasOne(x => x.DestinationCity);

        builder.HasOne(x => x.User);

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
