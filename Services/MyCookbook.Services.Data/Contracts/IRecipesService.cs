namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MyCookbook.Web.ViewModels.Recipes.Create;

    public interface IRecipesService
    {
        Task AddAsync(RecipeCreateServiceModel model);

        T GetById<T>(int recipeId);

        IEnumerable<T> GetAllFromCategory<T>(int categoryId, int? count = null);

        bool IsExistRecipeTitle(string recipeTtile);
    }
}
