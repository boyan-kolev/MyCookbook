﻿namespace MyCookbook.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCookbook.Web.ViewModels.Recipes.Create;
    using MyCookbook.Web.ViewModels.Recipes.Details;

    public interface IRecipesService
    {
        Task AddAsync(RecipeCreateServiceModel model);

        RecipeDetailsViewModel GetById(int recipeId, string userId, int countOfSimilarRecipes);

        IEnumerable<T> GetAllFromCategory<T>(int categoryId, int? count = null, int? withoutRecipeId = null);

        bool IsExistRecipeTitle(string recipeTtile);

        Task<bool> SetRecipeToUserFavoriteRecipesAsync(int recipeId, string userId);

        Task<bool> SetRecipeToUserCookedRecipesAsync(int recipeId, string userId);

        int GetCookTimesById(int recipeId);

        public IEnumerable<T> GetAll<T>();
    }
}
