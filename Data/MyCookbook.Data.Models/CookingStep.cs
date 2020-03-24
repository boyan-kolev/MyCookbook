namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyCookbook.Data.Common.Models;

    public class CookingStep : BaseDeletableModel<int>
    {
        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
