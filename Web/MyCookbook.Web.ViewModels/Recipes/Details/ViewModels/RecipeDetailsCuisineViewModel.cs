namespace MyCookbook.Web.ViewModels.Recipes.Details.ViewModels
{
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels;

    public class RecipeDetailsCuisineViewModel : IMapFrom<RecipeDetailsCuisineServiceModel>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
