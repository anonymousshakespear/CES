using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Value} DKK")]
public sealed record Price : IValueObject
{
    public Price(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "A price must be greater than zero");
        }

        Value = value;
    }

    public int Value { get; private set; }

    public static implicit operator Price(int price) => new(price);

    public static implicit operator int(Price price) => price.Value;
}
