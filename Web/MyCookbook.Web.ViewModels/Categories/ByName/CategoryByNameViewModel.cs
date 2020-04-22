namespace MyCookbook.Web.ViewModels.Categories.ByName
{
    using System.Collections.Generic;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class CategoryByNameViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public IEnumerable<RecipeInCategoryViewModel> Recipes { get; set; }
    }
}
