namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Common.Models;

    public class CookingMethod : BaseDeletableModel<int>
    {
        public CookingMethod()
        {
            this.RecipesCookingMethods = new HashSet<RecipeCookingMethod>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<RecipeCookingMethod> RecipesCookingMethods { get; set; }
    }
}
