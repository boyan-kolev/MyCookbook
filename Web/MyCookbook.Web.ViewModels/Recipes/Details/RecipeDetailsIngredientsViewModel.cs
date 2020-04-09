namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsIngredientsViewModel : IMapFrom<Ingredient>
    {
        public string Name { get; set; }
    }
}
