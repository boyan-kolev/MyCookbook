namespace MyCookbook.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyCookbook.Data.Models;

    public class UserFavoriteRecipeConfiguration : IEntityTypeConfiguration<UserFavoriteRecipe>
    {
        public void Configure(EntityTypeBuilder<UserFavoriteRecipe> builder)
        {
            builder.HasKey(x => new { x.RecipeId, x.UserId });

            builder.HasOne(x => x.Recipe)
                .WithMany(x => x.FavoritedBy)
                .HasForeignKey(x => x.RecipeId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.FavoriteRecipes)
                .HasForeignKey(x => x.UserId);
        }
    }
}
