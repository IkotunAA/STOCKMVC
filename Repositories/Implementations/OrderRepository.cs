using Microsoft.EntityFrameworkCore;
using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Repositories.Interfaces;

namespace STOCKMVC.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StockDbContext _context;

        public OrderRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public Order GetOrderById(string orderId)
        {
            var order = _context.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == orderId);
            return order;
        }

        public IEnumerable<Order> GetOrdersByCustomerId(string customerId)
        {
            var orders = _context.Orders.Include(o => o.Items).Where(o => o.CustomerId == customerId).ToList();
            return orders;
        }
    }

}
