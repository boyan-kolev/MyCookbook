namespace MyCookbook.Web.ViewModels.Recipes.Details.ViewModels
{
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels;

    public class RecipeDetailsIngredientsViewModel : IMapFrom<RecipeDetailsIngredientsServiceModel>
    {
        public string Name { get; set; }
    }
}
