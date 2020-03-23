namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
