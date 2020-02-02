namespace AnemicPizza.Domain.Models.Products
{
    public class Ingredient : Product
    {
        public bool IsSpicy { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public override ProductType Type => ProductType.Ingredient;
    }
}
