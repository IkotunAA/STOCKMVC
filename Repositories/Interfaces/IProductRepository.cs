using STOCKMVC.Entities;
using System.Linq.Expressions;

namespace STOCKMVC.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        Product GetProductById(string id);
        Product GetProductByName(string name);
        IEnumerable<Product> GetAllProducts();
        Product UpdateProduct(Product product);
        Product DeleteProduct(Product product);
    }
}
