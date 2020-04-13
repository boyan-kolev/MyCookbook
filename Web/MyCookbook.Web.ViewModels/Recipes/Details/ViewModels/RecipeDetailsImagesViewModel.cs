namespace MyCookbook.Web.ViewModels.Recipes.Details.ViewModels
{
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels;

    public class RecipeDetailsImagesViewModel : IMapFrom<RecipeDetailsImagesServiceModel>
    {
        public string Url { get; set; }

        public bool IsTitlePhoto { get; set; }
    }
}
