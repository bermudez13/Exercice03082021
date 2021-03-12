using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercice03082021.Core.Models;

namespace Exercice03082021.Persistence
{
    public class ApiContext: DbContext
    {
        #region DbSet
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

            modelBuilder.Entity<Order>()
                .HasKey(c => new { c.ProductID, c.UserID });
        }
    }
}