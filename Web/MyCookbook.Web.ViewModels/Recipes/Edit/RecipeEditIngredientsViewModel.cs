namespace MyCookbook.Web.ViewModels.Recipes.Edit
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeEditIngredientsViewModel : IMapFrom<Ingredient>
    {
        public string Name { get; set; }
    }
}
