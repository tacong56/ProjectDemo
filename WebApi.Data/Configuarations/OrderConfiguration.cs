using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data.Entities;

namespace WebApi.Data.Configuarations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.ShipAddress).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ShipName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ShipPhoneNumber).IsRequired().HasMaxLength(20);
            builder.HasOne(o => o.AppUser).WithMany(au => au.Orders).HasForeignKey(o => o.UserId);
        }
    }
}
