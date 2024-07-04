namespace STOCKMVC.Entities
{
    public class Product : Auditables
    {
        public string UserName { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal CostPrice { get; set; }
            public decimal SellingPrice { get; set; }
    }
}
