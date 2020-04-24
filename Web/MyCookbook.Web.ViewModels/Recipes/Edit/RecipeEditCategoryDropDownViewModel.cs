namespace MyCookbook.Web.ViewModels.Recipes.Edit
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeEditCategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}
