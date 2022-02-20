namespace ECommerceAPI.Models
{
    public class Carts : Bases
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
