namespace MyCookbook.Web.ViewModels.Recipes.All
{
    using System.Collections.Generic;

    using MyCookbook.Web.ViewModels.Partials;

    public class RecipeAllViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ListRecipesCollectionPartailViewModel> Recipes { get; set; }
    }
}
