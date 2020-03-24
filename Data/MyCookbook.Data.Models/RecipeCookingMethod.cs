namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class RecipeCookingMethod
    {
        public RecipeCookingMethod()
        {
            this.AddedOn = DateTime.UtcNow;
        }

        [Required]
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        [Required]
        public int CookingMethodId { get; set; }

        public virtual CookingMethod CookingMethod { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
