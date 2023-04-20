using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Value} km")]
public sealed record EdgeWheight : IValueObject
{
    public EdgeWheight(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "A distance must be greater than zero");
        }

        Value = value;
    }

    public int Value { get; private set; }

    public static implicit operator EdgeWheight(int distance) => new(distance);

    public static implicit operator int(EdgeWheight distance) => distance.Value;
}
