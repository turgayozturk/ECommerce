namespace ECommerceAPI.Models
{
    public class Products : Bases
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
        public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
