namespace MyCookbook.Web.ViewModels.Recipes.Edit
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeEditCookingMethodsCheckboxViewModel : IMapFrom<CookingMethod>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}
