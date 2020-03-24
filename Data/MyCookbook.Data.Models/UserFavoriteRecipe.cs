namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class UserFavoriteRecipe
    {
        public UserFavoriteRecipe()
        {
            this.AddedOn = DateTime.UtcNow;
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
