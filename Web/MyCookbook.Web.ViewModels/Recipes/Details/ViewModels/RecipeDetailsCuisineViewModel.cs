namespace MyCookbook.Web.ViewModels.Recipes.Details.ViewModels
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsCuisineViewModel : IMapFrom<Cuisine>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
