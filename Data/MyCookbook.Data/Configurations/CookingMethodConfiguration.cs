namespace MyCookbook.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyCookbook.Data.Models;

    public class CookingMethodConfiguration : IEntityTypeConfiguration<CookingMethod>
    {
        public void Configure(EntityTypeBuilder<CookingMethod> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
