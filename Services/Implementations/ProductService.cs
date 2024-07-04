using Microsoft.EntityFrameworkCore;
using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Models;
using STOCKMVC.Repositories.Implementations;
using STOCKMVC.Repositories.Interfaces;
using STOCKMVC.Services.Interfaces;

namespace STOCKMVC.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly StockDbContext _context;
        public ProductService (IProductRepository productRepository, IUnitOfWork unitOfWork, StockDbContext context)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public BaseResponse AddProduct(ProductRequestModel model)
        {
            var productExist = _productRepository.GetProductByName(model.ProductName);
            if (productExist != null)
            {
                return new BaseResponse
                {
                    Message = $"Product {model.ProductName} exist before"
                };
            }
            var product = new Product
            {
                UserName = model.UserName,  
                ProductName = model.ProductName,
                Quantity = model.Quantity,
                CostPrice = model.CostPrice,
                SellingPrice = model.SellingPrice,
            };
            _productRepository.AddProduct(product);
            _unitOfWork.SaveChanges();
            return new BaseResponse
            {
                Status = true,
                Message = $"Product {product.ProductName} Added Sucessfully"
            };
        }

        public BaseResponse DeleteProduct(string name)
        {
            var productExist = _productRepository.GetProductByName(name);
            if (productExist != null)
            {
                return new BaseResponse
                {
                    Message = $"Product {name} does not exist before"
                };
            }
            var product = new Product
            {
                ProductName = name
            };
            _productRepository.DeleteProduct(product);
            _unitOfWork.SaveChanges();
            return new BaseResponse
            {
                Status = true,
                Message = $"Product {product.ProductName} deleted Sucessfully"
            };
        }

        public IList<ProductResponse> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();

            var productResponses = products.Select(p => new ProductResponse
            {
                Status = true,
                Message = "Product retrieved successfully",
                Data = new ProductModel
                {
                    Id = p.Id,
                    UserName = p.UserName,
                    ProductName = p.ProductName,
                    Quantity = p.Quantity,
                    CostPrice = p.CostPrice,
                    SellingPrice = p.SellingPrice
                }
            }).ToList();

            return productResponses;
        }

        public ProductResponse GetProductById(string id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return new ProductResponse
                {
                    Message = $"Product with Id {id} Not found"
                };
            }

            return new ProductResponse
            {
                Data = new ProductModel
                {
                    Id = product.Id,
                    UserName = product.UserName,    
                    ProductName = product.ProductName,
                    Quantity = product.Quantity,
                    CostPrice = product.CostPrice,
                    SellingPrice = product.SellingPrice
                },
                Status = true,
                Message = "Product retrieved successfully"
            };
        }
        public ProductResponse GetProductByName(string name)
        {
            var product = _productRepository.GetProductByName(name);
            if (product == null)
            {
                return new ProductResponse
                {
                    Message = $"Product with Id {name} Not found"
                };
            }

            return new ProductResponse
            {
                Data = new ProductModel
                {
                    Id = product.Id,
                    UserName = product.UserName,
                    ProductName = product.ProductName,
                    Quantity = product.Quantity,
                    CostPrice = product.CostPrice,
                    SellingPrice = product.SellingPrice
                },
                Status = true,
                Message = "Product retrieved successfully"
            };
        }

        public BaseResponse RestockProduct(ProductRequestModel model)
        {
            var existingProduct = _productRepository.GetProductByName(model.ProductName);
            if (existingProduct == null)
            {
                return new BaseResponse
                {
                    Message = $"Product {model.ProductName} Not found"
                };
            }
            
            existingProduct.UserName = existingProduct.UserName;
            existingProduct.ProductName = existingProduct.ProductName;
            existingProduct.Quantity += model.Quantity;
            existingProduct.CostPrice = existingProduct.CostPrice;
            existingProduct.SellingPrice = existingProduct.SellingPrice;

            _productRepository.UpdateProduct(existingProduct);
            _unitOfWork.SaveChanges();
            return new BaseResponse
            {
                Status = true,
                Message = $"Product {existingProduct} updated successfully"
            };
        }
        public async Task ReduceProductQuantityAsync(string productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Quantity -= quantity;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
