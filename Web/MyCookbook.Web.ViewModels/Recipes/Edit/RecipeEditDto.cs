namespace MyCookbook.Web.ViewModels.Recipes.Edit
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Data.Models.Enums;

    public class RecipeEditDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string Description { get; set; }

        public string Advices { get; set; }

        public int Servings { get; set; }

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public Seasonal SeasonalType { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public int CategoryId { get; set; }

        public int CuisineId { get; set; }

        public IEnumerable<IFormFile> NewImages { get; set; }

        public string IngredientsNames { get; set; }

        public RecipeEditCookingMethodsCheckboxViewModel[] CookingMethods { get; set; }
    }
}
