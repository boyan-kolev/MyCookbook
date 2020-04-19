namespace MyCookbook.Web.ViewModels.Recipes.Search
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeSearchCategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
