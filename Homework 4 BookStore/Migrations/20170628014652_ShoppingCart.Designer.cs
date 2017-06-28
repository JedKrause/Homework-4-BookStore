using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Homework_4_BookStore.Data;

namespace Homework4BookStore.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20170628014652_ShoppingCart")]
    partial class ShoppingCart
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Homework_4_BookStore.Models.Book", b =>
                {
                    b.Property<int>("BookID");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Path");

                    b.Property<decimal>("Price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("BookID");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("Homework_4_BookStore.Models.Patron", b =>
                {
                    b.Property<int>("PatronID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("MembershipDate");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("PermissionsLevel");

                    b.HasKey("PatronID");

                    b.ToTable("Patron");
                });

            modelBuilder.Entity("Homework_4_BookStore.Models.Rental", b =>
                {
                    b.Property<int>("RentalID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookID");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("PatronID");

                    b.HasKey("RentalID");

                    b.HasIndex("BookID");

                    b.HasIndex("PatronID");

                    b.ToTable("Rental");
                });

            modelBuilder.Entity("Homework_4_BookStore.Models.ShoppingCart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookID");

                    b.Property<string>("Path");

                    b.Property<int>("PatronID");

                    b.Property<decimal>("Price");

                    b.Property<int>("Qty");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Homework_4_BookStore.Models.Rental", b =>
                {
                    b.HasOne("Homework_4_BookStore.Models.Book", "Book")
                        .WithMany("Rentals")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Homework_4_BookStore.Models.Patron", "Patron")
                        .WithMany("Rentals")
                        .HasForeignKey("PatronID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
