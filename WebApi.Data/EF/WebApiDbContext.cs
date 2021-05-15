using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Data.Entities;
using WebApi.Data.Extensions;

namespace WebApi.Data.EF
{
    public class WebApiDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public WebApiDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data seeding
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
