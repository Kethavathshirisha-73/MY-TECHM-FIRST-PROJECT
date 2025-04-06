namespace EcommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public string? ShippingAddress { get; set; }
        public string? Notes { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
