using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Value} Hours")]
public sealed record Time : IValueObject
{
    public Time(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "A time must be greater than zero");
        }

        Value = value;
    }

    public int Value { get; private set; }

    public static implicit operator Time(int time) => new(time);

    public static implicit operator int(Time time) => time.Value;
}
