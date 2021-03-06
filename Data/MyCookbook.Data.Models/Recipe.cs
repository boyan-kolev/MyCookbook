﻿namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyCookbook.Data.Common.Models;
    using MyCookbook.Data.Models.Enums;

    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.Images = new HashSet<Image>();
            this.Ingredients = new HashSet<Ingredient>();
            this.RecipesCookingMethods = new HashSet<RecipeCookingMethod>();
            this.CookedBy = new HashSet<UserCookedRecipe>();
            this.FavoritedBy = new HashSet<UserFavoriteRecipe>();
            this.Ratings = new HashSet<Rating>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(800)]
        public string Advices { get; set; }

        [Required]
        public int Servings { get; set; }

        [Required]
        public int PrepTime { get; set; }

        [Required]
        public int CookTime { get; set; }

        [Required]
        public Seasonal SeasonalType { get; set; }

        [Required]
        public SkillLevel SkillLevel { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public int CuisineId { get; set; }

        public virtual Cuisine Cuisine { get; set; }

        public bool IsApproved { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public virtual ICollection<RecipeCookingMethod> RecipesCookingMethods { get; set; }

        public virtual ICollection<UserCookedRecipe> CookedBy { get; set; }

        public virtual ICollection<UserFavoriteRecipe> FavoritedBy { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
