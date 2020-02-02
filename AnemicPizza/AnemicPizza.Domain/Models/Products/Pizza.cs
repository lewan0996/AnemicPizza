﻿using System.Collections.Generic;
using System.Linq;

namespace AnemicPizza.Domain.Models.Products
{
    public class Pizza : Product
    {
        public override ProductType Type => ProductType.Pizza;
        public override int AvailableQuantity => 
            Ingredients.Min(pi => pi.Ingredient.AvailableQuantity);
        public IList<PizzaIngredient> Ingredients { get; set; }
    }
}
