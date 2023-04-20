
namespace RoutePlanning.Application.Locations.Queries.City;

public sealed record SelectableCity(Domain.Locations.Models.City.EntityId cityID, string name, bool status, DateTime rowVersion);
