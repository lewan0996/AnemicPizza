using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Exceptions;
using AnemicPizza.Core.Models.Products;
using AnemicPizza.Core.Repositories;
using AnemicPizza.Core.Services.Interfaces;

namespace AnemicPizza.Core.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IRepository<Pizza> _pizzaRepository;
        private readonly IRepository<Ingredient> _ingredientRepository;

        public PizzaService(IRepository<Pizza> pizzaRepository, IRepository<Ingredient> ingredientRepository)
        {
            _pizzaRepository = pizzaRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Pizza> CreatePizzaAsync(string name, string description, float unitPrice, int[] ingredientIds)
        {
            var pizza = new Pizza(name, description, unitPrice);
            
            foreach (var ingredientId in ingredientIds)
            {
                var ingredient = await GetIngredientTask(ingredientId);
                pizza.Ingredients.Add(new PizzaIngredient {Ingredient = ingredient, Pizza = pizza});
            }

            pizza.Validate();

            await _pizzaRepository.AddAsync(pizza);

            return pizza;
        }

        public async Task DeletePizzaAsync(int id)
        {
            var pizzaToDelete = await _pizzaRepository.GetByIdAsync(id);
            if (pizzaToDelete == null)
            {
                throw new RecordNotFoundException(id, nameof(Pizza));
            }

            _pizzaRepository.Delete(pizzaToDelete);
        }

        public async Task UpdatePizzaAsync(int id, string name, string description, float? unitPrice, int[] ingredientIds = null)
        {
            var pizzaToUpdate = await _pizzaRepository.GetByIdAsync(id);

            if (pizzaToUpdate == null)
            {
                throw new RecordNotFoundException(id, nameof(Pizza));
            }

            if (name != null)
            {
                pizzaToUpdate.Name = name;
            }

            if (description != null)
            {
                pizzaToUpdate.Description = description;
            }

            if (unitPrice.HasValue)
            {
                pizzaToUpdate.UnitPrice = unitPrice.Value;
            }

            if (ingredientIds != null)
            {
                var ingredients = new List<Ingredient>();
                foreach (var ingredientId in ingredientIds)
                {
                    ingredients.Add(await GetIngredientTask(ingredientId));
                }

                pizzaToUpdate.Ingredients.Clear();

                foreach (var ingredient in ingredients)
                {
                    pizzaToUpdate.Ingredients.Add(new PizzaIngredient {Ingredient = ingredient, Pizza = pizzaToUpdate});
                }
            }

            pizzaToUpdate.Validate();
        }

        public Task<Pizza> GetPizzaByIdAsync(int id)
        {
            return _pizzaRepository.GetByIdAsync(id);
        }

        public Task<IReadOnlyList<Pizza>> GetAllPizzasAsync()
        {
            return _pizzaRepository.GetAllAsync();
        }

        private async Task<Ingredient> GetIngredientTask(int ingredientId)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(ingredientId);
            if (ingredient == null)
            {
                throw new RecordNotFoundException(ingredientId, nameof(Ingredient));
            }

            return ingredient;
        }
    }
}
