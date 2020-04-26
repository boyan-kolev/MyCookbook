namespace MyCookbook.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    using MyCookbook.Web.ViewModels.Partials;

    public class LastCreatedRecipesViewModel
    {
        public IEnumerable<LastCreatedRecipeViewModel> Recipes { get; set; }
    }
}
