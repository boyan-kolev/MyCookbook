namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyCookbook.Data.Common.Models;

    public class CookingMethod : BaseDeletableModel<int>
    {
        public CookingMethod()
        {
            this.RecipesCookingMethods = new HashSet<RecipeCookingMethod>();
        }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<RecipeCookingMethod> RecipesCookingMethods { get; set; }
    }
}
