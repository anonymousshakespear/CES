
using System.Runtime.InteropServices;
using RoutePlanning.Domain.Locations.Models;

namespace RoutePlanning.Domain.Locations.Repository;
public class ProductCategoryRepository
{
    public ProductCategory getProductCategoryByID(int id)
    {
        return new ProductCategory();
    }

    public void saveProductCategory(ProductCategory productCategory) { }
    public void deleteProductCategory(int id) { }
}
