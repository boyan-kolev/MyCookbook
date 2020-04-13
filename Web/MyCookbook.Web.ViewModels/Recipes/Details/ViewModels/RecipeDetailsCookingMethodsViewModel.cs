namespace MyCookbook.Web.ViewModels.Recipes.Details.ViewModels
{
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels;

    public class RecipeDetailsCookingMethodsViewModel : IMapFrom<RecipeDetailsCookingMethodsServiceModel>
    {
        public string Name { get; set; }
    }
}
