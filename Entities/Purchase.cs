namespace STOCKMVC.Entities
{
    public class Purchase : Auditables
    { 
            public string UserName { get; set; }
            public string ProductName { get; set; }
            public string ProductId { get; set; }
        public Product Product { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
    }
}
