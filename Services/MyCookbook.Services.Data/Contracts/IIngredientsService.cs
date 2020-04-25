namespace MyCookbook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IIngredientsService
    {
        Task SetIngredientToRecipeAsync(string ingredientName, int recipeId);

        Task DeleteIngredientsByRecipeIdAsync(int recipeId);
    }
}
