using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data.Entities;

namespace WebApi.Data.Configuarations
{
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.HasOne(pt => pt.Product).WithMany(p => p.ProductTranslations).HasForeignKey(pt => pt.ProductId);
            builder.HasOne(pt => pt.Language).WithMany(l => l.ProductTranslations).HasForeignKey(pt => pt.LanguageId);
        }
    }
}
