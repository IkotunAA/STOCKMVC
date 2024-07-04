using Microsoft.EntityFrameworkCore;
using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace STOCKMVC.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly StockDbContext _context;

        public CartRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByCustomerIdAsync(string customerId)
        {
            return await _context.Carts.Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(c => c.CustomerId == customerId)
                   ?? new Cart { CustomerId = customerId, Items = new List<CartItem>() };
        }

        public async Task<Cart> AddToCartAsync(string customerId, CartItem item)
        {
            var cart = await GetCartByCustomerIdAsync(customerId);
            var existingItem = _context.CartItems.FirstOrDefault(c => c.CustomerName == item.CustomerName && c.ProductId == item.ProductId);

            if (existingItem == null)
            {
                //item.CustomerName = cart.CustomerName;
                _context.CartItems.Add(item);
            }
            else
            {
                existingItem.Quantity += item.Quantity;
                existingItem.Price += item.Price;
                _context.CartItems.Update(existingItem);
            }

            await SaveCartAsync(cart);
            return cart;
        }

        public async Task RemoveFromCartAsync(string customerId, string productId)
        {
            var cart = await GetCartByCustomerIdAsync(customerId);
            var itemToRemove = cart.Items.FirstOrDefault(i => i.CustomerName == cart.CustomerName && i.ProductId == productId);

            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                await SaveCartAsync(cart);
            }
        }

        public async Task ClearCartAsync(string customerId)
        {
            var cart = await GetCartByCustomerIdAsync(customerId);
            cart.Items.Clear();
            await SaveCartAsync(cart);
        }

        public async Task SaveCartAsync(Cart cart)
        {
            if (_context.Carts.Any(c => c.Id == cart.Id))
            {
                _context.Carts.Update(cart);
            }
            else
            {
                await _context.Carts.AddAsync(cart);
            }
            await _context.SaveChangesAsync();
        }
    }
}
