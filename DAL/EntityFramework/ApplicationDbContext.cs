using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions opt) : base(opt)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // base.OnModelCreating(modelBuilder);
           //Form this function, we can do the association for User to UserMap, UserProfile to UserProfileMap and CategoryMap to Category
           new UserMap(modelBuilder.Entity<User>());
           new UserProfileMap(modelBuilder.Entity<UserProfile>());
           new CategoryMap(modelBuilder.Entity<Category>());
        }
    }
}
