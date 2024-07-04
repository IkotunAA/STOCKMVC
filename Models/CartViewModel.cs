namespace STOCKMVC.Models
{
    public class CartViewModel
    {
        public IList<CartItemModel> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
