namespace MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsImagesServiceModel : IMapFrom<Image>
    {
        public string Url { get; set; }

        public bool IsTitlePhoto { get; set; }
    }
}
