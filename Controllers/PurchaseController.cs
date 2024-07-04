//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using STOCKMVC.Entities;
//using STOCKMVC.Services.Interfaces;

//[Authorize(Roles = "BusinessOwner,Customer")]
//public class PurchaseController : Controller
//{
//    private readonly IPurchaseService _purchaseService;
//    private readonly IProductService _productService;

//    public PurchaseController(IPurchaseService purchaseService, IProductService productService)
//    {
//        _purchaseService = purchaseService;
//        _productService = productService;
//    }

//    [HttpGet]
//    public IActionResult Index()
//    {
//        var purchases = _purchaseService.GetAllPurchases();
//        return View(purchases);
//    }

//    [HttpGet]
//    public IActionResult ByDateRange(DateTime startDate, DateTime endDate)
//    {
//        var purchases = _purchaseService.GetPurchasesByDateRange(startDate, endDate);
//        return View(purchases);
//    }

//    [HttpGet]
//    public IActionResult PurchaseProduct(string productName)
//    {
//        var product = _productService.GetAllProducts().FirstOrDefault(p => p.Data.ProductName == productName);
//        return View(product);
//    }

//    [HttpPost]
//    public IActionResult PurchaseProduct(string productName, int quantity, decimal price)
//    {
//        if (ModelState.IsValid)
//        {
//            _purchaseService.PurchaseProduct(User.Identity.Name, productName, quantity, price);
//            return RedirectToAction("Index");
//        }
//        return View();
//    }
//}
