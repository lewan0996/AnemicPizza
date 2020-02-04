using System.Collections.Generic;
using AnemicPizza.API.DTO.Ingredient;
using AutoMapper;

namespace AnemicPizza.API.DTO.Pizza
{
    [AutoMap(typeof(Core.Models.Products.Pizza))]
    public class PizzaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float UnitPrice { get; set; }
        public int AvailableQuantity { get; set; } //todo make as computed column
        public List<IngredientDTO> Ingredients { get; set; }
    }
}
