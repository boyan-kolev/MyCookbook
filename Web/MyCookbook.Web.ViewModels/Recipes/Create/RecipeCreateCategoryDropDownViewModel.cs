namespace MyCookbook.Web.ViewModels.Recipes.Create
{

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeCreateCategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
