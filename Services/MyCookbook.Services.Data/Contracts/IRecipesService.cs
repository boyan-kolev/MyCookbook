namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MyCookbook.Services.Models.Recipes;

    public interface IRecipesService
    {
        Task AddAsync(RecipeCreateServiceModel model);

        T GetById<T>(int recipeId);

        bool IsExistRecipeTitle(string recipeTtile);
    }
}
