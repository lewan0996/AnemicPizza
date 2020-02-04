namespace AnemicPizza.Core.Models.Products
{
    public class Ingredient : Product
    {
        public bool IsSpicy { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public override ProductType Type => ProductType.Ingredient;

        public Ingredient(string name, string description, float unitPrice, int availableQuantity, bool isSpicy,
            bool isVegetarian, bool isVegan) : base(name, description, unitPrice, availableQuantity)
        {
            IsSpicy = isSpicy;
            IsVegetarian = isVegetarian;
            IsVegan = isVegan;
        }

        protected Ingredient() // For EF
        {

        }
    }
}
