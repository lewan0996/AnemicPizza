using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Products;
using AnemicPizza.Core.Services.Interfaces;

namespace AnemicPizza.Core.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IRepository<Ingredient> _repository;

        public IngredientService(IRepository<Ingredient> repository)
        {
            _repository = repository;
        }

        public async Task<Ingredient> CreateIngredientAsync(string name, string description, float unitPrice, int availableQuantity, bool isSpicy,
            bool isVegetarian, bool isVegan)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(new ArgumentException("Product name can't be empty.", nameof(name)));
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new DomainException(new ArgumentException("Product name can't be empty.", nameof(description)));
            }
            if (availableQuantity <= 0)
            {
                throw new DomainException(new ArgumentException("The quantity of the product must be greater than 0",
                    nameof(availableQuantity)));
            }

            var ingredient = new Ingredient(
                name,
                description,
                unitPrice,
                availableQuantity,
                isSpicy,
                isVegetarian,
                isVegan);

            await _repository.AddAsync(ingredient);

            return ingredient;
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredientToDelete = await _repository.GetByIdAsync(id);

            if (ingredientToDelete == null)
            {
                throw new RecordNotFoundException(id, nameof(Ingredient));
            }

            _repository.Delete(ingredientToDelete);
        }

        public async Task UpdateIngredientAsync(int id, string name, string description, float? unitPrice, int? availableQuantity, bool? isSpicy,
            bool? isVegetarian, bool? isVegan)
        {
            var ingredientToUpdate = await _repository.GetByIdAsync(id);

            if (ingredientToUpdate == null)
            {
                throw new RecordNotFoundException(id, nameof(Ingredient));
            }

            if (name != null)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new DomainException(new ArgumentException("Product name can't be empty.", nameof(name)));
                }
                ingredientToUpdate.Name = name;
            }

            if (description != null)
            {
                if (string.IsNullOrWhiteSpace(description))
                {
                    throw new DomainException(new ArgumentException("Product name can't be empty.", nameof(description)));
                }
                ingredientToUpdate.Description = description;
            }

            if (availableQuantity.HasValue)
            {
                if (availableQuantity <= 0)
                {
                    throw new DomainException(new ArgumentException("The quantity of the product must be greater than 0",
                        nameof(availableQuantity)));
                }

                if (availableQuantity > ingredientToUpdate.AvailableQuantity)
                {
                    ingredientToUpdate.AvailableQuantity += availableQuantity.Value -
                                                            ingredientToUpdate.AvailableQuantity;
                }
                if (availableQuantity < ingredientToUpdate.AvailableQuantity)
                {
                    ingredientToUpdate.AvailableQuantity-=ingredientToUpdate.AvailableQuantity -
                                                         availableQuantity.Value;
                }
            }

            if (unitPrice.HasValue)
            {
                ingredientToUpdate.UnitPrice = unitPrice.Value;
            }

            if (isSpicy.HasValue)
            {
                ingredientToUpdate.IsSpicy = isSpicy.Value;
            }

            if (isVegetarian.HasValue)
            {
                ingredientToUpdate.IsVegetarian = isVegetarian.Value;
            }

            if (isVegan.HasValue)
            {
                ingredientToUpdate.IsVegan = isVegan.Value;
            }
        }

        public Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IReadOnlyList<Ingredient>> GetAllIngredientsAsync()
        {
            return _repository.GetAllAsync();
        }
    }
}
