using System.ComponentModel.DataAnnotations;

namespace AnemicPizza.API.DTO.Pizza
{
    public class CreatePizzaDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0, float.MaxValue)]
        public float UnitPrice { get; set; }
        public int[] IngredientIds { get; set; }
    }
}
