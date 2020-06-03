using Microsoft.EntityFrameworkCore;
using CustomerDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

 namespace CustomerDemo.Context
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomerDbContext(
            DbContextOptions<CustomerDbContext> dbContextOptions) 
            : base(dbContextOptions) {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(x => x.Id);
        }
    }
}
