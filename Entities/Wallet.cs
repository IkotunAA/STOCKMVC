namespace STOCKMVC.Entities
{
    public class Wallet : Auditables
    {
        public string UserName { get; set; }
        public decimal amount { get; set; }
        public decimal Balance { get; set; } = 0m;
    }
}
