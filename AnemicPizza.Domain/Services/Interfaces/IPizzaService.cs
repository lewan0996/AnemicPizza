using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Products;

namespace AnemicPizza.Core.Services.Interfaces
{
    public interface IPizzaService
    {
        Task<Pizza> CreatePizzaAsync(string name, string description, float unitPrice, int[] ingredientIds);

        Task DeletePizzaAsync(int id);

        Task UpdatePizzaAsync(int id, string name, string description, float? unitPrice,
            int[] ingredientIds = null);

        Task<Pizza> GetPizzaByIdAsync(int id);
        Task<IReadOnlyList<Pizza>> GetAllPizzasAsync();
    }
}
