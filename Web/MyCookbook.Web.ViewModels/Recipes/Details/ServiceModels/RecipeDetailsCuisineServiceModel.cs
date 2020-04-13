namespace MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsCuisineServiceModel : IMapFrom<Cuisine>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
