﻿// <auto-generated />
using System;
using CocktailDb.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace cocktailDb.Migrations
{
    [DbContext(typeof(CocktailContext))]
    [Migration("20240501205825_TablesAdjusted")]
    partial class TablesAdjusted
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("cocktailDb.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("cocktailDb.Models.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Alcoholic")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("AlternateName")
                        .HasColumnType("longtext");

                    b.Property<string>("Category")
                        .HasColumnType("longtext");

                    b.Property<string>("DbDrinkId")
                        .HasColumnType("longtext");

                    b.Property<int?>("GlassTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Iba")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Instructions")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("GlassTypeId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("cocktailDb.Models.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EmailAdress")
                        .HasColumnType("longtext");

                    b.Property<int>("EmailsSent")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("cocktailDb.Models.Glass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Glasses");
                });

            modelBuilder.Entity("cocktailDb.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DrinkName")
                        .HasColumnType("longtext");

                    b.Property<int>("IdOfDrink")
                        .HasColumnType("int");

                    b.Property<string>("Ingredient1")
                        .HasColumnType("longtext");

                    b.Property<string>("Ingredient2")
                        .HasColumnType("longtext");

                    b.Property<string>("Ingredient3")
                        .HasColumnType("longtext");

                    b.Property<string>("Ingredient4")
                        .HasColumnType("longtext");

                    b.Property<string>("Ingredient5")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("IdOfDrink")
                        .IsUnique();

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("cocktailDb.Models.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DrinkName")
                        .HasColumnType("longtext");

                    b.Property<int>("IdOfDrink")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Measure1")
                        .HasColumnType("longtext");

                    b.Property<string>("Measure2")
                        .HasColumnType("longtext");

                    b.Property<string>("Measure3")
                        .HasColumnType("longtext");

                    b.Property<string>("Measure4")
                        .HasColumnType("longtext");

                    b.Property<string>("Measure5")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdOfDrink")
                        .IsUnique();

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("cocktailDb.Models.Drink", b =>
                {
                    b.HasOne("cocktailDb.Models.Glass", "GlassType")
                        .WithMany()
                        .HasForeignKey("GlassTypeId");

                    b.Navigation("GlassType");
                });

            modelBuilder.Entity("cocktailDb.Models.Ingredient", b =>
                {
                    b.HasOne("cocktailDb.Models.Drink", "Drink")
                        .WithOne("Ingredients")
                        .HasForeignKey("cocktailDb.Models.Ingredient", "IdOfDrink")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drink");
                });

            modelBuilder.Entity("cocktailDb.Models.Measurement", b =>
                {
                    b.HasOne("cocktailDb.Models.Drink", "Drink")
                        .WithOne("Measurements")
                        .HasForeignKey("cocktailDb.Models.Measurement", "IdOfDrink")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drink");
                });

            modelBuilder.Entity("cocktailDb.Models.Drink", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Measurements");
                });
#pragma warning restore 612, 618
        }
    }
}
