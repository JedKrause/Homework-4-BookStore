using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Homework_4_BookStore.Models;

namespace Homework_4_BookStore.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Rental>().ToTable("Rental");
            modelBuilder.Entity<Patron>().ToTable("Patron");
            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCarts");

        }

    }
}
