using STOCKMVC.Entities;

namespace STOCKMVC.Models
{
    public class PurchaseModel
    {
        public string Id { get; set; }  
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class PurchaseRequestModel
    {
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class PurchasesRequesModel
    {
        public string ProductName { get; set; }
        //public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
