using STOCKMVC.Models;

namespace STOCKMVC.Entities
{
    public class Cart : Auditables
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        //public string CustomerName => Items.FirstOrDefault(item => item.c)
        public decimal TotalPrice => Items.Sum(item => item.Price);
    }


}
