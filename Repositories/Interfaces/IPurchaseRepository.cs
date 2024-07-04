using STOCKMVC.Entities;

namespace STOCKMVC.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        Purchase AddPurchase(Purchase purchase);
        IEnumerable<Purchase> GetPurchasesByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<Purchase> GetPurchasesByUserName(string userName);
        IEnumerable<Purchase> GetAllPurchase();
    }
}
