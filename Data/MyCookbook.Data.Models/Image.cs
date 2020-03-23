namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public string Url { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
