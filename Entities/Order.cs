namespace STOCKMVC.Entities
{
    public class Order : Auditables
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }

}
