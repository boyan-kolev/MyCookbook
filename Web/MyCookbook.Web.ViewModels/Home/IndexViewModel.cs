namespace MyCookbook.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using MyCookbook.Web.ViewModels.Partials;

    public class IndexViewModel
    {
        public IEnumerable<ListRecipesCollectionPartailViewModel> TopRecipes { get; set; }

        public IEnumerable<IndexCategoriesViewModel> Categories { get; set; }

        public IEnumerable<IndexCuisinesViewModel> Cuisines { get; set; }
    }
}
