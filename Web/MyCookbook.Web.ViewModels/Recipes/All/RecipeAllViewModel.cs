namespace MyCookbook.Web.ViewModels.Recipes.All
{
    using MyCookbook.Web.ViewModels.Recipes.Search;
    using System.Collections.Generic;

    public class RecipeAllViewModel
    {
        public IEnumerable<RecipeAllRecipesViewModel> Recipes { get; set; }

        public RecipeSearchInputModel Filter { get; set; }
    }
}
