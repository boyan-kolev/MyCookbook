namespace MyCookbook.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyCookbook.Data.Models;

    public class RecipeCookingMethodConfiguration : IEntityTypeConfiguration<RecipeCookingMethod>
    {
        public void Configure(EntityTypeBuilder<RecipeCookingMethod> builder)
        {
            builder.HasKey(x => new { x.RecipeId, x.CookingMethodId });

            builder.HasOne(x => x.Recipe)
                .WithMany(x => x.RecipesCookingMethods)
                .HasForeignKey(x => x.RecipeId);

            builder.HasOne(x => x.CookingMethod)
                .WithMany(x => x.RecipesCookingMethods)
                .HasForeignKey(x => x.CookingMethodId);
        }
    }
}
