namespace MyCookbook.Web.ViewModels.Recipes.Edit
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeEditImagesViewModel : IMapFrom<Image>
    {
        public string Url { get; set; }

        public bool IsTitlePhoto { get; set; }
    }
}
