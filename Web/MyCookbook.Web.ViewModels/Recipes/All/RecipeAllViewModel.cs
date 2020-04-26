﻿namespace MyCookbook.Web.ViewModels.Recipes.All
{
    using System.Collections.Generic;

    using MyCookbook.Web.ViewModels.Partials;

    public class RecipeAllViewModel
    {
        public IEnumerable<ListRecipesCollectionPartailViewModel> Recipes { get; set; }
    }
}
