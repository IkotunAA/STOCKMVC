using STOCKMVC.Models;
using STOCKMVC.Models.DTOs;

namespace STOCKMVC.Services.Interfaces
{
    public interface IProductService
    {
            BaseResponse AddProduct(ProductRequestModel model);
            ProductResponse GetProductById(string id);
            ProductResponse GetProductByName(string name);
        IList<ProductResponse> GetAllProducts();
        Task ReduceProductQuantityAsync(string productId, int quantity);
        BaseResponse RestockProduct(ProductRequestModel model);
            BaseResponse DeleteProduct(string name);

    }
}
