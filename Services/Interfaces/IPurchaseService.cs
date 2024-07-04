using STOCKMVC.Models;

namespace STOCKMVC.Services.Interfaces
{
    public interface IPurchaseService
    {
        BaseResponse PurchaseProduct(string userName, string productId, string productName, int quantity, decimal price);
        IEnumerable<PurchaseRequestModel> GetPurchasesByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<PurchaseRequestModel> GetPurchasesByUserName(string username);
        IEnumerable<PurchaseModel> GetAllPurchases();


    }
}
