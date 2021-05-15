using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data.Entities;

namespace WebApi.Data.Configuarations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.HasOne(pi => pi.Product).WithMany(p => p.ProductImages).HasForeignKey(pi => pi.ProductId);
        }
    }
}
