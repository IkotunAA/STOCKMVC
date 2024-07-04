using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOCKMVC.Entities;
using STOCKMVC.Models;
using STOCKMVC.Services.Interfaces;
using System.Security.Claims;

[Authorize(Policy = "RequireBusinessOwnerRole")]
public class BusinessOwnerController : Controller
{
    private readonly IProductService _productService;
    private readonly IPurchaseService _purchaseService;
    private readonly IWalletService _walletService;

    public BusinessOwnerController(IProductService productService, IPurchaseService purchaseService, IWalletService walletService)
    {
        _productService = productService;
        _purchaseService = purchaseService;
        _walletService = walletService;
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateProduct(ProductRequestModel model)
    {
        //if (ModelState.IsValid)
        //{
        //    try
        //    {
        model.UserName = User.Identity.Name;
        _productService.AddProduct(model);
                return RedirectToAction("Dashboard");
            //}
        //    catch (Exception ex)
        //    {
        //        //_logger.LogError(ex, "Error creating product");
        //        ModelState.AddModelError("", "An error occurred while creating the product.");
        //    }
        //}
        return View(model);
    }

    public IActionResult StockProduct()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StockProduct(ProductRequestModel model)
    {
        //if (ModelState.IsValid)
        //{
            _productService.RestockProduct(model);
            return RedirectToAction("Dashboard");
        //}
        //return View(model);
    }

    public IActionResult SeeAllPurchases()
    {
        var purchases = _purchaseService.GetAllPurchases();
        return View(purchases);
    }

    public IActionResult SeePurchasesByDate()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SeePurchasesByDate(DateTime startDate, DateTime endDate)
    {
        var purchases = _purchaseService.GetPurchasesByDateRange(startDate, endDate);
        return View("SeeAllPurchases", purchases);
    }

    public IActionResult SeePurchasesByName()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SeePurchasesByName(string customerName)
    {
        var purchases = _purchaseService.GetPurchasesByUserName(customerName);
        return View("SeeAllPurchases", purchases);
    }

    public IActionResult GetProfitOrLossByDate()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetProfitOrLossByDate(DateTime startDate, DateTime endDate)
    {
        var profitOrLoss = _purchaseService.GetPurchasesByDateRange(startDate, endDate);
        return View("ProfitOrLoss", profitOrLoss);
    }

    //public IActionResult GetProfitOrLossByProduct()
    //{
    //    return View();
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult GetProfitOrLossByProduct(string productId)
    //{
    //    var profitOrLoss = _purchaseService.(productId);
    //    return View("ProfitOrLoss", profitOrLoss);
    //}

    //public IActionResult AddPurchasedGoodsSoldMoneyToWallet()
    //{
    //    return View();
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    public IActionResult WalletBalance()
    {
        var purchases = _purchaseService.GetAllPurchases();
        var price = purchases.Sum(purchase => purchase.Price);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _walletService.TopUpWallet(userId, price);
        return RedirectToAction("Dashboard");
    }
}
