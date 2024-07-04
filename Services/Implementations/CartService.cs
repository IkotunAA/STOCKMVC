using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Models;
using STOCKMVC.Repositories.Interfaces;
using STOCKMVC.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STOCKMVC.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly StockDbContext _context;

        public CartService(ICartRepository cartRepository, IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, StockDbContext context)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<CartitemResponse> GetCartAsync(string customerId)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                return new CartitemResponse
                {
                    Message = "No cart found"
                };
            }

            var cartItem = cart.Items.FirstOrDefault();
            if (cartItem == null)
            {
                return new CartitemResponse
                {
                    Message = "Cart is empty"
                };
            }

            return new CartitemResponse
            {
                CustomerName = cart.CustomerName,
                ProductName = cartItem.Product.ProductName,
                Price = cartItem.Price,
                Quantity = cartItem.Quantity,
                Message = "This is the cart item"
            };
        }

        public async Task AddToCartAsync(string customerId, string customerName, CartItem item)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    CustomerName = customerName,
                    Items = new List<CartItem>()
                };
                await _context.Carts.AddAsync(cart);
                //_cartRepository.AddToCartAsync(customerId, Items);

            }
            _cartRepository.AddToCartAsync(customerId,item);

            //var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            //if (existingItem == null)
            //{
            //    item.Id = Guid.NewGuid().ToString();
            //    cart.Items.Add(item);
            //}
            //else
            //{
            //    existingItem.Quantity += item.Quantity;
            //    existingItem.Price = item.Price; // Assuming price may change
            //}

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItemModel>> GetCartItemsByCustomerIdAsync(string customerId)
        {
            var cartItems = await _context.CartItems
                .Where(ci => ci.UserId == customerId)
                .Select(ci => new CartItemModel
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.ProductName,
                    Quantity = ci.Quantity,
                    Price = ci.Product.SellingPrice
                })
                .ToListAsync();

            return cartItems;
        }

        public async Task RemoveFromCartAsync(string customerId, string cartId)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            var itemToRemove = cart.Items.FirstOrDefault(i => i.Id == cartId);

            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string customerId)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            cart.Items.Clear();
            await _context.SaveChangesAsync();
        }

        public async Task<Order> CheckoutAsync(string customerId, string customerName)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart.Items.Count == 0)
            {
                throw new InvalidOperationException("Cart is empty.");
            }

            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CustomerId = customerId,
                CustomerName = customerName,
                Items = cart.Items,
                TotalPrice = cart.Items.Sum(i => i.Quantity * i.Price),
                OrderDate = DateTime.Now,
                Status = "Pending"
            };

            await _orderRepository.AddOrderAsync(order);
            await ClearCartAsync(customerId);
            await _unitOfWork.SaveChangesAsync();

            foreach (var item in order.Items)
            {
                await ReduceProductQuantityAsync(item.ProductId, item.Quantity);
            }

            return order;
        }

        public async Task ReduceProductQuantityAsync(string productId, int quantity)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            product.Quantity -= quantity;
            if (product.Quantity < 0)
            {
                throw new InvalidOperationException("Insufficient product quantity.");
            }

            await _context.SaveChangesAsync();
        }
    }
}
