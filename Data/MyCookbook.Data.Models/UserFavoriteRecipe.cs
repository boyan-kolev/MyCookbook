namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserFavoriteRecipe
    {
        public UserFavoriteRecipe()
        {
            this.AddedOn = DateTime.UtcNow;
        }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
