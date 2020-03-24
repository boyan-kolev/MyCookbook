namespace MyCookbook.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyCookbook.Data.Models;

    public class UserCookedRecipeConfiguration : IEntityTypeConfiguration<UserCookedRecipe>
    {
        public void Configure(EntityTypeBuilder<UserCookedRecipe> builder)
        {
            builder.HasKey(x => new { x.RecipeId, x.UserId });

            builder.HasOne(x => x.Recipe)
                .WithMany(x => x.CookedBy)
                .HasForeignKey(x => x.RecipeId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.CookedRecipes)
                .HasForeignKey(x => x.UserId);
        }
    }
}
