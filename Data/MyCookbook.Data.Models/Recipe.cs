namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Common.Models;
    using MyCookbook.Data.Models.Enums;

    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.Images = new HashSet<Image>();
            this.Ingredients = new HashSet<Ingredient>();
            this.CookingSteps = new HashSet<CookingStep>();
            this.RecipesCookingMethods = new HashSet<RecipeCookingMethod>();
        }

        public string Title { get; set; }

        public int Servings { get; set; }

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public Seasonal SeasonalType { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public int AuthorId { get; set; }

        public ApplicationUser MyProperty { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int CuisineId { get; set; }

        public Cuisine Cuisine { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }

        public ICollection<CookingStep> CookingSteps { get; set; }

        public ICollection<RecipeCookingMethod> RecipesCookingMethods { get; set; }
    }
}
