using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations.Models;
public class ProductCategory : AggregateRoot<ProductCategory>
{
    public string category { get; set; }
    public float cost { get; set; }
    public DateTime rowVersion { get; set; }

    public ProductCategory(string category, float cost, DateTime rowVersion)
    {
        this.category = category;
        this.cost = cost;
        this.rowVersion = rowVersion;
    }
}
