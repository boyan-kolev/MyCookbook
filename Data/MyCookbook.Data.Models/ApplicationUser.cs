// ReSharper disable VirtualMemberCallInConstructor
namespace MyCookbook.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using MyCookbook.Data.Common.Models;
    using MyCookbook.Data.Models.Enums;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Recipes = new HashSet<Recipe>();
            this.CookedRecipes = new HashSet<UserCookedRecipe>();
            this.FavoritedBy = new HashSet<UserFavoriteRecipe>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        // Additional info
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public Gender Gender { get; set; }

        public string ProfilePhoto { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<UserCookedRecipe> CookedRecipes { get; set; }

        public virtual ICollection<UserFavoriteRecipe> FavoritedBy { get; set; }
    }
}
