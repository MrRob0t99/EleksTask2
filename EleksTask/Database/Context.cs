﻿using EleksTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TourServer.Models;

namespace EleksTask
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BasketProduct>()
                .HasKey(t => new { t.ProductId, t.ApplicationUserId });

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<EmailToken> EmailTokens { get; set; }

        public DbSet<BasketProduct> BasketProducts { get; set; }
    }


}
