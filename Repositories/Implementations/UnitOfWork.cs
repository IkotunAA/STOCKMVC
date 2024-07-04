using Microsoft.EntityFrameworkCore;
using STOCKMVC.Context;
using STOCKMVC.Repositories.Interfaces;

namespace STOCKMVC.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockDbContext _context;
        public UnitOfWork(StockDbContext context)
        {
            _context = context;
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

