namespace OnlineShop.Domain.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; } 
        public virtual ICollection<Order> Orders { get; set; }

    }
}
