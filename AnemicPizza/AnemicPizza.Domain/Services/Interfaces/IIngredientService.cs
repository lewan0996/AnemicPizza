using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Products;

namespace AnemicPizza.Core.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<Ingredient> CreateIngredientAsync(string name, string description, float unitPrice, int availableQuantity,
            bool isSpicy, bool isVegetarian, bool isVegan);

        Task DeleteIngredientAsync(int id);

        Task UpdateIngredientAsync(int id, string name, string description, float? unitPrice,
            int? availableQuantity,
            bool? isSpicy, bool? isVegetarian, bool? isVegan);

        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task<IReadOnlyList<Ingredient>> GetAllIngredientsAsync();
    }
}
