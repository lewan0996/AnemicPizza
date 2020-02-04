namespace AnemicPizza.API.DTO.Pizza
{
    public class UpdatePizzaDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float? UnitPrice { get; set; }
        public int[] IngredientIds { get; set; }
    }
}
