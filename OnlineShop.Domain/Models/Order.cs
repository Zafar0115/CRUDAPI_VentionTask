namespace OnlineShop.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public double OrderAmount { get; set; }
        public DateTime OrderDate { get; init; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; }
    }
}
