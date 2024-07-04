using STOCKMVC.Entities;

namespace STOCKMVC.Repositories.Interfaces
{
    public interface IWalletRepository
    {
        Wallet AddWallet(Wallet wallet);
        Wallet TopUpWallet(Wallet wallet);
        Wallet GetWalletByUserName(string userName);
        //Wallet GetBalance(Wallet wallet);
    }
}
