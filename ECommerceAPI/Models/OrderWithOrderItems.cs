namespace ECommerceAPI.Models
{
    public class OrderWithOrderItems
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Orders Orders { get; set; }

        public List<OrderItems> OrderItems { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
