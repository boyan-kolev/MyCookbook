namespace MyCookbook.Web.ViewModels.Partials
{
    using System.Collections.Generic;

    public class ListRecipesPartialViewModel
    {
        public IEnumerable<ListRecipesCollectionPartailViewModel> Recipes { get; set; }
    }
}
