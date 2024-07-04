using Microsoft.AspNetCore.Http.HttpResults;
using STOCKMVC.Entities;
using STOCKMVC.Models;
using STOCKMVC.Repositories.Interfaces;
using STOCKMVC.Services.Interfaces;

namespace STOCKMVC.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IPurchaseRepository purchaseRepository, IProductRepository productRepository, IWalletRepository walletRepository, IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }

        public BaseResponse PurchaseProduct(string userName, string productId, string productName, int quantity, decimal price)
        {
            var product = _productRepository.GetProductByName(productName);
            if (product == null || product.Quantity < quantity)
            {
                return new BaseResponse
                {
                    Message = $"The product {productName} is not available"
                };
            }
            //var wallet = _walletRepository.GetWalletByUserName(userName);
            //var totalPrice = quantity * price;

            //if (wallet.Balance < totalPrice)
            //{
            //    return new BaseResponse
            //    {
            //        Message = $"Your wallet balance is {wallet.Balance} is low and the total price is {totalPrice}",
            //    };
            //}

            var purchase = new Purchase
            {
                ProductId = productId,
                UserName = userName,
                ProductName = productName,
                Quantity = quantity,
                Price = price,
            };

            _purchaseRepository.AddPurchase(purchase);
            

            // Update product quantity
            //product.Quantity -= quantity;
            //_productRepository.UpdateProduct(product);

            // Update wallet balance
            //wallet.Balance -= totalPrice;
            //var newWalletBalance = new Wallet
            //{
            //    UserName = userName,
            //    Balance = wallet.Balance,
            //};
            //_walletRepository.TopUpWallet(newWalletBalance);
            _unitOfWork.SaveChanges();            return new BaseResponse
            {
                Status = true,
                Message = "Purchase Made Successfully"
            };


        }

        public IEnumerable<PurchaseRequestModel> GetPurchasesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _purchaseRepository.GetPurchasesByDateRange(startDate, endDate).Select(p => new PurchaseRequestModel
            {
                UserName = p.UserName,
                ProductName = p.ProductName,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList();
        }

        public IEnumerable<PurchaseRequestModel> GetPurchasesByUserName(string username)
        {
            return _purchaseRepository.GetPurchasesByUserName(username).Select(p => new PurchaseRequestModel
            {
                CreatedAt = p.CreatedAt,
                UserName = p.UserName,
                ProductName = p.ProductName,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList();
        }

        public IEnumerable<PurchaseModel> GetAllPurchases()
        {
            return _purchaseRepository.GetAllPurchase().Select(p => new PurchaseModel
            {
                Id = p.Id,
                UserName = p.UserName,
                ProductName = p.ProductName,
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList();
        }
    }

}
