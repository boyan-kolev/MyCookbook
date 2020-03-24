namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyCookbook.Data.Common.Models;

    public class Cuisine : BaseDeletableModel<int>
    {
        public Cuisine()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
