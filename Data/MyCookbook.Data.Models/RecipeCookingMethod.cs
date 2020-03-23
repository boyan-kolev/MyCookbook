namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RecipeCookingMethod
    {
        public RecipeCookingMethod()
        {
            this.AddedOn = DateTime.UtcNow;
        }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public int CookingMethodId { get; set; }

        public CookingMethod CookingMethod { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
