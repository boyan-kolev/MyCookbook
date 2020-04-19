namespace MyCookbook.Web.ViewModels.Recipes.Search
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeSearchCuisineDropDownViewModel : IMapFrom<Cuisine>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
