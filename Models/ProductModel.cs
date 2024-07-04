namespace STOCKMVC.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
    }
    public class ProductRequestModel
    {
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
    }
    public class ProductResponse : BaseResponse
    {
        public ProductModel Data { get; set; }
        

    }
    //public class ProductsResponse : BaseResponse
    //{
    //    public IEnumerable<ProductModel> data { get; set; }
    //}
}
