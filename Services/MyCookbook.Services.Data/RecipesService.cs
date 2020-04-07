namespace MyCookbook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Models.Recipes;
    using MyCookbook.Web.ViewModels.CookingMethods;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IIngredientsService ingredientsService;

        public RecipesService(IDeletableEntityRepository<Recipe> recipesRepository, IIngredientsService ingredientsService)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsService = ingredientsService;
        }

        public async Task AddAsync(RecipeCreateServiceModel model)
        {
            var recipe = new Recipe()
            {
                Title = model.Title,
                Description = model.Description,
                Advices = model.Advices,
                Servings = model.Servings,
                PrepTime = model.PrepTime,
                CookTime = model.CookTime,
                SeasonalType = model.SeasonalType,
                SkillLevel = model.SkillLevel,
                AuthorId = model.AuthorId,
                CategoryId = model.CategoryId,
                CuisineId = model.CuisineId,
            };

            await this.recipesRepository.AddAsync(recipe);

            var imageUrls = model.ImageUrls;
            var titleImageUrl = model.TitleImageUrl;

            foreach (var imageUrl in imageUrls)
            {
                var image = new Image()
                {
                    Url = imageUrl,
                    RecipeId = recipe.Id,
                };
            }

            var titleImage = new Image()
            {
                Url = titleImageUrl,
                IsTitlePhoto = true,
                RecipeId = recipe.Id,
            };

            var ingredientsNames = model.IngredientsNames.Split("\r\n");

            foreach (var ingredientName in ingredientsNames)
            {
                await this.ingredientsService.SetIngredientToRecipeAsync(ingredientName, recipe.Id);
            }

            SetcookingMethodsToRecipes(model.CookingMethods, recipe.Id);
        }

        private void SetcookingMethodsToRecipes(CookingMethodsCheckboxViewModel[] cookingMethods, int id)
        {
            foreach (var cookingMethod in cookingMethods)
            {

            }
        }
    }
}
