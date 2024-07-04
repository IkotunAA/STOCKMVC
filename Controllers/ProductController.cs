//// Controllers/ProductController.cs
//using Microsoft.AspNetCore.Mvc;
//using STOCKMVC.Models;
//using STOCKMVC.Models.DTOs;
//using STOCKMVC.Services.Interfaces;

//public class ProductController : Controller
//{
//    private readonly IProductService _productService;

//    public ProductController(IProductService productService)
//    {
//        _productService = productService;
//    }

//    public IActionResult Index()
//    {
//        var products = _productService.GetAllProducts();
//        return View(products);
//    }

//    public IActionResult Create()
//    {
//        return View();
//    }

//    [HttpPost]
//    public IActionResult Create(ProductRequestModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            _productService.AddProduct(model);
//            return RedirectToAction(nameof(Index));
//        }
//        return View(model);
//    }

//    public IActionResult RestockProduct(string name)
//    {
//        var product = _productService.GetProductByName(name);
//        if (product == null) return NotFound();

//        return View(product);
//    }

//    [HttpPost]
//    public IActionResult RestockProduct(ProductRequestModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            _productService.RestockProduct(model);
//            return RedirectToAction(nameof(Index));
//        }
//        return View(model);
//    }

//    public IActionResult Delete(string name)
//    {
//        var product = _productService.GetProductByName(name);
//        if (product == null) return NotFound();

//        return View(product);
//    }

//    [HttpPost, ActionName("Delete")]
//    public IActionResult DeleteConfirmed(string name)
//    {
//        _productService.DeleteProduct(name);
//        return RedirectToAction(nameof(Index));
//    }
//}
