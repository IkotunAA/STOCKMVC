using STOCKMVC.Entities;
using ZstdSharp.Unsafe;

namespace STOCKMVC.Models
{
    public class CartItemModel
    {
        //public string ProductId { get; set; }
        //public string ProductName { get; set; }
        //public int Quantity { get; set; }
        //public decimal Price { get; set; }
        public string CartId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; } 
    }
    public class CartitemResponse : BaseResponse
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
    public class CartResponse : BaseResponse
    {
        public string cartId {  get; set; } 
        public List<CartitemResponse> Items { get; set; } = new List<CartitemResponse>();
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
    }

}
