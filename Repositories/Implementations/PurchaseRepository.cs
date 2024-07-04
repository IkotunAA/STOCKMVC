using Microsoft.EntityFrameworkCore;
using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Repositories.Interfaces;

namespace STOCKMVC.Repositories.Implementations
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly StockDbContext _context;

        public PurchaseRepository(StockDbContext context)
        {
            _context = context;
        }
        public Purchase AddPurchase(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            return purchase;
        }

        public IEnumerable<Purchase> GetAllPurchase()
        {
            return _context.Purchases
                          .Include(p => p.Product)
                          .ToList();
        }

        public IEnumerable<Purchase> GetPurchasesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Purchases
                           .Include(p => p.Product)
                           .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
                           .ToList();
        }

        public IEnumerable<Purchase> GetPurchasesByUserName(string userName)
        {
            return _context.Purchases
                           .Include(p => p.Product)
                           .Where(p => p.UserName == userName)
                           .ToList();
        }
    }
}
