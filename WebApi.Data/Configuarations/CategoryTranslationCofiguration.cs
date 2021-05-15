using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data.Entities;

namespace WebApi.Data.Configuarations
{
    class CategoryTranslationCofiguration : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.HasOne(ct => ct.Category).WithMany(c => c.CategoryTranslations).HasForeignKey(ct => ct.CategoryId);
            builder.HasOne(ct => ct.Language).WithMany(l => l.CategoryTranslations).HasForeignKey(ct => ct.LanguageId);
        }
    }
}
