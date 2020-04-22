namespace MyCookbook.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexTopRecipesViewModel> TopRecipes { get; set; }

        public IEnumerable<IndexCategoriesViewModel> Categories { get; set; }

        public IEnumerable<IndexCuisinesViewModel> Cuisines { get; set; }
    }
}
