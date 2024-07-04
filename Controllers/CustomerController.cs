using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOCKMVC.Entities;
using STOCKMVC.Models;
using STOCKMVC.Services.Interfaces;
using System.Security.Claims;

[Authorize(Policy = "RequireCustomerRole")]
public class CustomerController : Controller
{
    private readonly IProductService _productService;
    private readonly IWalletService _walletService;
    private readonly IPurchaseService _purchaseService;
    private readonly ICartService _cartService;

    public CustomerController(IProductService productService, IWalletService walletService, IPurchaseService purchaseService, ICartService cartService)
    {
        _productService = productService;
        _walletService = walletService;
        _purchaseService = purchaseService;
        _cartService = cartService;
    }
    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult AvailableProducts()
    {
        var products = _productService.GetAllProducts();
        return View(products);
    }
    public async Task<IActionResult> PurchaseCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var cartItems = (await _cartService.GetCartItemsByCustomerIdAsync(userId)).ToList();

        var asd = cartItems.Select(a => new CartItemModel
        {
            ProductName = a.ProductName,
            Price = a.Price,
            Quantity = a.Quantity,
            TotalPrice = cartItems.Sum(ci => ci.Quantity * ci.Price),
        }).ToList();

        return View(asd);
        //  foreach (var item in cartItems)
        //  {
        //      var viewModel = new CartItemModel
        //      {
        //          ProductName = item.ProductName,
        //          Quantity = item.Quantity,
        //          Price = item.Price, 
        //          TotalPrice = cartItems.Sum(ci => ci.Quantity * ci.Price)
        //      };
        //      return View(viewModel);
        //  }
        //return View();


    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(string productId, int quantity)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var product =  _productService.GetProductById(productId);
        var userName = User.FindFirstValue(ClaimTypes.Name);

        if (product != null)
        {
            var cartItem = new CartItem
            {
                UserId = userId,
                CustomerName = userName,
                ProductId = productId,
                Quantity = quantity,
                Price = product.Data.SellingPrice,
                CreatedAt = DateTime.UtcNow
            };

            await _cartService.AddToCartAsync(userId, userName, cartItem);
        }

        return RedirectToAction("Dashboard");
    }

[HttpGet]
    public IActionResult TopUpWallet()
    {
        return View();
    }

    [HttpPost]
    public IActionResult TopUpWallet(decimal amount)
    {
       var userName = User.FindFirstValue(ClaimTypes.Name);
        _walletService.TopUpWallet(userName, amount);
        return RedirectToAction("WalletBalance");
    }

    public async Task<IActionResult> WalletBalance()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var walletBalance = await _walletService.GetWalletBalance(userName);
        return View(walletBalance);
    }

    public async Task<IActionResult> RemoveFromCart(string cartId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
       // var cartItems = await _cartService.GetCartItemsByCustomerIdAsync(userId);
       //foreach (var item in cartItems)
       // {
            await _cartService.RemoveFromCartAsync(userId, cartId);
        //}
       
        return View("PurchaseCart");
    }
    public async Task<IActionResult> ClearCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _cartService.ClearCartAsync(userId);
        return RedirectToAction("PurchaseCart");
    }
    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.FindFirstValue(ClaimTypes.Name);

        var cartItems = await _cartService.GetCartItemsByCustomerIdAsync(userId);

        var totalAmount = cartItems.Sum(ci => ci.Quantity * ci.Price);
        var walletBalance = await _walletService.GetWalletBalance(userName);

        if (walletBalance < totalAmount)
        {
            TempData["Error"] = "Insufficient funds in your wallet.";
            return RedirectToAction("PurchaseCart");
        }

        var newWalletBalance = walletBalance - totalAmount;
        await _walletService.UpdateWalletBalance(userName, newWalletBalance);

        foreach (var cartItem in cartItems)
        {
        
            await _productService.ReduceProductQuantityAsync(cartItem.ProductId, cartItem.Quantity);
            _purchaseService.PurchaseProduct(userName, cartItem.ProductId, cartItem.ProductName, cartItem.Quantity, cartItem.Price);

           
            TempData["Success"] = $"Successfully purchased {cartItem.Quantity} units of {cartItem.ProductName}.";
            await _cartService.CheckoutAsync(userId, userName);
            await _cartService.RemoveFromCartAsync(userId, cartItem.CartId);
        }

        return RedirectToAction("PurchaseHistory");
    }


    public IActionResult PurchaseHistory()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var purchases = _purchaseService.GetPurchasesByUserName(userName);
        return View(purchases);
    }

    [HttpGet]
    public IActionResult PurchaseHistoryByDate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult PurchaseHistoryByDate(DateTime startDate, DateTime endDate)
    {
        //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var purchases = _purchaseService.GetPurchasesByDateRange(startDate, endDate);
        return View("PurchaseHistory", purchases);
    }
}
