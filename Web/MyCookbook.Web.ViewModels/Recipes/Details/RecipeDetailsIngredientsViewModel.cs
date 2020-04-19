namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsIngredientsViewModel : IMapFrom<Ingredient>
    {
        public string Name { get; set; }
    }
}
