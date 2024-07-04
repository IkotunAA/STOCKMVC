using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Repositories.Interfaces;

namespace STOCKMVC.Repositories.Implementations
{
    public class WalletRepository : IWalletRepository
    {
        protected readonly StockDbContext _context;
        public WalletRepository(StockDbContext context)
        {
            _context = context; 
        }
        public Entities.Wallet AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            return wallet;

        }

        //public Wallet GetBalance(Wallet wallet)
        //{
        //    _context.Wallets.(wallet);
        //}

        public Wallet GetWalletByUserName(string userName)
        {
            var userWallet = _context.Wallets.FirstOrDefault(x => x.UserName == userName);
            return userWallet;
        }

        public Wallet TopUpWallet(Wallet wallet)
        {
          _context.Wallets.Update(wallet);
            //_context.SaveChanges();                         
            return wallet;
        }
    }
}
