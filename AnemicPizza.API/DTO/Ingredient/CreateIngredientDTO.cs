using System.ComponentModel.DataAnnotations;
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable 1591

namespace AnemicPizza.API.DTO.Ingredient
{
    public class CreateIngredientDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0,float.MaxValue)]
        public float UnitPrice { get; set; }
        [Range(0,int.MaxValue)]
        public int AvailableQuantity { get; set; }
        public bool IsSpicy { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
    }
}
