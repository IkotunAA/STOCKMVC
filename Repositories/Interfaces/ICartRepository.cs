using STOCKMVC.Entities;
using STOCKMVC.Models;

namespace STOCKMVC.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByCustomerIdAsync(string customerId);
        Task<Cart> AddToCartAsync(string customerId, CartItem item);
        Task RemoveFromCartAsync(string customerId, string productId);
        Task ClearCartAsync(string customerId);
        Task SaveCartAsync(Cart cart);
    }
}
