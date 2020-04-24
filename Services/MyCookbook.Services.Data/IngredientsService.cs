namespace MyCookbook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;

    public class IngredientsService : IIngredientsService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public IngredientsService(IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public void DeleteIngredientsFromRecipe(int recipeId)
        {
            var ingredientsInRecipe = this.ingredientRepository
                .All()
                .Where(i => i.RecipeId == recipeId)
                .ToList();

            foreach (var ingredient in ingredientsInRecipe)
            {
                this.ingredientRepository.Delete(ingredient);
            }
        }

        public async Task SetIngredientToRecipeAsync(string ingredientName, int recipeId)
        {
            var ingredient = new Ingredient()
            {
                Name = ingredientName,
                RecipeId = recipeId,
            };

            await this.ingredientRepository.AddAsync(ingredient);
        }
    }
}
