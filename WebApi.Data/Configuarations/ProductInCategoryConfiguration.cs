using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data.Entities;

namespace WebApi.Data.Configuarations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");
            builder.HasKey(x => new { x.ProductId, x.CategoryId });
            builder.HasOne(pic => pic.Category).WithMany(c => c.productInCategories).HasForeignKey(pic => pic.CategoryId);
            builder.HasOne(pic => pic.Product).WithMany(p => p.productInCategories).HasForeignKey(pic => pic.ProductId);
        }
    }
}
