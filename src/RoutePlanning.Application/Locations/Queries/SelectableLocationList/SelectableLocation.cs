using Netcompany.Net.DomainDrivenDesign.Models;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Queries.SelectableLocationList;

public sealed record SelectableLocation(Entity<Location, Guid>.EntityId LocationId, string Name);
