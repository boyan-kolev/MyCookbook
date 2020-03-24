namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyCookbook.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        [Required]
        public string Url { get; set; }

        public bool IsTitlePhoto { get; set; }

        [Required]
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
