namespace STOCKMVC.Entities
{
    public class CartItem : Auditables
    {
        public string UserId { get; set; }
        public string CustomerName { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
    }
}
