using STOCKMVC.Models;

namespace STOCKMVC.Services.Interfaces
{
    public interface IWalletService
    {
 
         BaseResponse TopUpWallet(string userId, decimal amount);
        Task<decimal> GetWalletBalance(string userName);
        Task UpdateWalletBalance(string userName, decimal newBalance);
            //void AddToWallet(string userId, decimal amount);

    }
}
