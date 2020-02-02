namespace AnemicPizza.Domain.Models.Ordering
{
    public class Supplier : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SupplierStatus Status { get; set; }
    }

    public enum SupplierStatus
    {
        Free = 1,
        Occupied = 2
    }
}
