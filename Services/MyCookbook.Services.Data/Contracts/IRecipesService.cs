namespace MyCookbook.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyCookbook.Web.ViewModels.Recipes.Create;
    using MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels;

    public interface IRecipesService
    {
        Task AddAsync(RecipeCreateServiceModel model);

        RecipeDetailsServiceModel GetById(int recipeId, int countOfSimilarRecipes);

        IEnumerable<T> GetAllFromCategory<T>(int categoryId, int? count = null, int? withoutRecipeId = null);

        bool IsExistRecipeTitle(string recipeTtile);
    }
}
