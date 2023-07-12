﻿// <auto-generated />
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230712222400_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("Core.Entities.RecipeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RecipeCategories");
                });

            modelBuilder.Entity("Core.Entities.RecipePost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Picture")
                        .HasColumnType("TEXT");

                    b.Property<int>("RecipeCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RecipeCategoryId");

                    b.ToTable("RecipePosts");
                });

            modelBuilder.Entity("Core.Entities.RecipeStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecipePostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Step")
                        .HasColumnType("TEXT");

                    b.Property<int>("StepNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("RecipeSteps");
                });

            modelBuilder.Entity("Core.Entities.RecipePost", b =>
                {
                    b.HasOne("Core.Entities.RecipeCategory", "RecipeCategory")
                        .WithMany()
                        .HasForeignKey("RecipeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecipeCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
