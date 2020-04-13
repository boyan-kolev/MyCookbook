namespace MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsIngredientsServiceModel : IMapFrom<Ingredient>
    {
        public string Name { get; set; }
    }
}
