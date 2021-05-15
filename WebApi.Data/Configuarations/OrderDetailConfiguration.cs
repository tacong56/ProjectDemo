using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data.Entities;

namespace WebApi.Data.Configuarations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => new { x.ProductId, x.OrderId });
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.HasOne(od => od.Order).WithMany(o => o.OrderDetails).HasForeignKey(od => od.OrderId);
            builder.HasOne(od => od.Product).WithMany(p => p.OrderDetails).HasForeignKey(od => od.ProductId);
        }
    }
}
