﻿// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable 1591
namespace AnemicPizza.API.DTO.Ingredient
{
    public class UpdateIngredientDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float? UnitPrice { get; set; }
        public int? AvailableQuantity { get; set; }
        public bool? IsSpicy { get; set; }
        public bool? IsVegetarian { get; set; }
        public bool? IsVegan { get; set; }
    }
}
