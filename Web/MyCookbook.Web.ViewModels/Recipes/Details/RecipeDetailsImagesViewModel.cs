namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsImagesViewModel : IMapFrom<Image>
    {
        public string Url { get; set; }

        public bool IsTitlePhoto { get; set; }
    }
}
