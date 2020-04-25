﻿namespace MyCookbook.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCookbook.Web.ViewModels.Recipes.Create;
    using MyCookbook.Web.ViewModels.Recipes.Details;
    using MyCookbook.Web.ViewModels.Recipes.Edit;
    using MyCookbook.Web.ViewModels.Recipes.Filtered;

    public interface IRecipesService
    {
        Task<int> AddAsync(RecipeCreateServiceModel model);

        Task<int> EditAsync(RecipeEditDto model);

        Task DeleteAsync(int recipeId);

        RecipeDetailsViewModel GetByIdForDetails(int recipeId, string userId, int countOfSimilarRecipes);

        RecipeEditInputModel GetByIdForEdit(int recipeId);

        IEnumerable<T> GetAllFromCategory<T>(int categoryId, int? count = null, int? withoutRecipeId = null);

        bool IsExistRecipeTitle(string recipeTtile);

        Task<bool> SetRecipeToUserFavoriteRecipesAsync(int recipeId, string userId);

        Task<bool> SetRecipeToUserCookedRecipesAsync(int recipeId, string userId);

        int GetCookTimesById(int recipeId);

        IEnumerable<T> GetAll<T>();

        RecipeFilteredViewModel GetFiltered(RecipeFilteredInputDto input);

        IEnumerable<T> GetLastCreatedRecipes<T>(int count);

        IEnumerable<T> GetTopRecipes<T>(int count);

        bool IsExistRecipe(int id);

        string GetRecipeTitle(int recipeId);
    }
}
