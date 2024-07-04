using STOCKMVC.Entities;
using STOCKMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace STOCKMVC.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartitemResponse> GetCartAsync(string customerId);
        Task AddToCartAsync(string customerId, string customerName, CartItem item);
        Task<IEnumerable<CartItemModel>> GetCartItemsByCustomerIdAsync(string customerId);
        Task RemoveFromCartAsync(string customerId, string cartId);
        Task ClearCartAsync(string customerId);
        Task<Order> CheckoutAsync(string customerId,string customerName);
        Task ReduceProductQuantityAsync(string productId, int quantity);
    }
}
