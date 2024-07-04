using Microsoft.EntityFrameworkCore;
using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Models;
using STOCKMVC.Repositories.Interfaces;
using STOCKMVC.Services.Interfaces;

namespace STOCKMVC.Services.Implementations
{
    public class WalletService : IWalletService
    {
            private readonly IWalletRepository _walletRepository;
            private readonly IUnitOfWork _unitOfWork;
        private readonly StockDbContext _context;
            public WalletService(IWalletRepository walletRepository, IUnitOfWork unitOfWork, StockDbContext context)
            {
                _walletRepository = walletRepository;
                _unitOfWork = unitOfWork;
                _context = context;
            }
        public async Task<decimal> GetWalletBalance(string userName)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserName == userName);
            return wallet?.Balance ?? 0;
        }

        public async Task UpdateWalletBalance(string userName, decimal newBalance)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserName == userName);
            if (wallet != null)
            {
                wallet.Balance = newBalance;
                await _context.SaveChangesAsync();
            }
        }

        public BaseResponse TopUpWallet(string userName, decimal amount)
        {
            var wallet = _walletRepository.GetWalletByUserName(userName);
            wallet.Balance += amount;
            _walletRepository.TopUpWallet(wallet);
            _unitOfWork.SaveChanges();
            return new BaseResponse
            {
                Status = true,
                Message = $"Your Account has been top up with {amount} and your new balance is {wallet.Balance}"
            };
            
        }

        //public Wallet UpdateWalletBalance(string userName, decimal newBalance)
        //{
        //    var wallet = _walletRepository.GetWalletByUserName(userName);
        //    if (wallet == null)
        //    {
        //        throw new Exception("Wallet not found");
        //    }
        //    wallet.Balance = newBalance;
        //    return new Wallet
        //    {
        //        UserName = userName,
        //        Balance = newBalance,
        //    };
        //}
    }
}
