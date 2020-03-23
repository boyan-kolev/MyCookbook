namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Common.Models;

    public class CookingStep : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
