namespace MyCookbook.Web.ViewModels.Recipes.Create
{

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeCreateCuisineDropDownViewModel : IMapFrom<Cuisine>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
