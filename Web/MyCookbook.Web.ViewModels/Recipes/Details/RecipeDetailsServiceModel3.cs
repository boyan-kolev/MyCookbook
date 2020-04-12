namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsServiceModel3 : IMapFrom<Recipe>
    {
        public string Title { get; set; }
    }
}
