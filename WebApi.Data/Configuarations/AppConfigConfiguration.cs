using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data.Entities;

namespace WebApi.Data.Configuarations
{
    public class AppConfigConfiguration : IEntityTypeConfiguration<AppConfig>
    {
        public void Configure(EntityTypeBuilder<AppConfig> builder)
        {
            builder.ToTable("AppConfigs"); //Set name of table in database
            builder.HasKey(x => x.Key); //Set Key for table
            builder.Property(x => x.Key).UseMySqlIdentityColumn(); //Set identity for property Key
            builder.Property(x => x.Value).IsRequired(); //Set not null for property Key
        }
    }
}
