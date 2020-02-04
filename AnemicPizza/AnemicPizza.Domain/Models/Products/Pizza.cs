using System.Collections.Generic;
using System.Linq;

namespace AnemicPizza.Core.Models.Products
{
    public class Pizza : Product
    {
        public override ProductType Type => ProductType.Pizza;
        public override int AvailableQuantity => 
            Ingredients.Min(pi => pi.Ingredient.AvailableQuantity);
        public IList<PizzaIngredient> Ingredients { get; set; }

        public Pizza(string name, string description, float unitPrice) : base(name,description,unitPrice)
        {
            Ingredients = new List<PizzaIngredient>();
        }

        protected Pizza() // For EF
        {

        }
    }
}
