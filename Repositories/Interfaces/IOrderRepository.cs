using STOCKMVC.Entities;

namespace STOCKMVC.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Order GetOrderById(string orderId);
        IEnumerable<Order> GetOrdersByCustomerId(string customerId);
    }

}
