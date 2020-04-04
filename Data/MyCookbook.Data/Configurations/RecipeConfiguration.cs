namespace MyCookbook.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyCookbook.Data.Models;

    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Title)
                .IsUnique();

            builder.HasMany(x => x.Images)
                .WithOne(x => x.Recipe)
                .HasForeignKey(x => x.RecipeId);

            builder.HasMany(x => x.Ingredients)
                .WithOne(x => x.Recipe)
                .HasForeignKey(x => x.RecipeId);
        }
    }
}
