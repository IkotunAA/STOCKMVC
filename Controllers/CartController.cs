//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using STOCKMVC.Entities;
//using STOCKMVC.Models;
//using STOCKMVC.Repositories.Interfaces;
//using STOCKMVC.Services.Interfaces;

//namespace STOCKMVC.Controllers
//{
//    [Authorize(Roles = "Customer")]
//    public class CartController : Controller
//    {
//        private readonly ICartService _cartService;
//        private readonly IOrderRepository _orderRepository;

//        public CartController(ICartService cartService, IOrderRepository orderRepository)
//        {
//            _cartService = cartService;
//            _orderRepository = orderRepository;
//        }

//        public IActionResult Index()
//        {
//            var customerId = User.Identity.Name;
//            var cart = _cartService.GetCart(customerId);
//            return View(cart);
//        }

//        [HttpPost]
//        public IActionResult AddToCart(string productId, string productName, decimal price, int quantity)
//        {
//            var customerId = User.Identity.Name;
//            var item = new CartItem
//            {
//                ProductId = productId,
//                //ProductName = productName,
//                Price = price,
//                Quantity = quantity
//            };
//            _cartService.AddToCart(customerId, item);
//            return RedirectToAction("Index");
//        }
//        //public IActionResult Index()
//        //{
//        //    var customerId = User.Identity.Name;
//        //    var cart = _cartService.GetCartItemsByCustomerId(customerId);
//        //    return View(cart);
//        //}

//        public IActionResult CartItems()
//        {
//            var customerId = User.Identity.Name;
//            var items = _cartService.GetCartItemsByCustomerId(customerId);
//            return View(items);
//        }
    

//    [HttpPost]
//        public IActionResult RemoveFromCart(string productId)
//        {
//            var customerId = User.Identity.Name;
//            _cartService.RemoveFromCart(customerId, productId);
//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public IActionResult ClearCart()
//        {
//            var customerId = User.Identity.Name;
//            _cartService.ClearCart(customerId);
//            return RedirectToAction("Index");
//        }

//        public IActionResult Checkout()
//        {
//            var customerId = User.Identity.Name;
//            var order = _cartService.Checkout(customerId);
//            return RedirectToAction("OrderConfirmation", new { orderId = order.Items.First().Product.Name });
//        }

//        public IActionResult OrderConfirmation(string orderId)
//        {
//            var order = _orderRepository.GetOrderById(orderId);
//            return View(order);
//        }
//    }
//}
