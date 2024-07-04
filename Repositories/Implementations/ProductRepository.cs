using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Repositories.Interfaces;
using System.Linq.Expressions;

namespace STOCKMVC.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly StockDbContext _context;

        public ProductRepository(StockDbContext context)
        {
            _context = context;
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            return product;
        }

        public Product DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            return product;

        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(string id)
        {
           return _context.Products.Find(id);
        }

        public Product GetProductByName(string name)
        {
           var product = _context.Products.FirstOrDefault(x => x.ProductName == name);
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            return product;
        }
    }
}
