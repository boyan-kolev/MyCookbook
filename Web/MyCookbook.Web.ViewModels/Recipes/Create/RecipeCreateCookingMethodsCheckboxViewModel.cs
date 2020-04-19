namespace MyCookbook.Web.ViewModels.Recipes.Create
{

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeCreateCookingMethodsCheckboxViewModel : IMapFrom<CookingMethod>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}
