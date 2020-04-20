namespace MyCookbook.Web.ViewModels.Recipes.Filtered
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeFilteredCuisineViewModel : IMapFrom<Cuisine>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
