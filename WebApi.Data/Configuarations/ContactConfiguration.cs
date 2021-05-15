using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data.Entities;

namespace WebApi.Data.Configuarations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contatcs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
        }
    }
}
