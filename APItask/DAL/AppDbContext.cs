﻿using APItask.Entities;
using Microsoft.EntityFrameworkCore;

namespace APItask.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}
