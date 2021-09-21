﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEDC.NotesApp.Models;

namespace SEDC.NotesApp.Domain.Migrations
{
    [DbContext(typeof(NotesAppDbContext))]
    [Migration("20210921183303_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SEDC.NotesApp.Models.DbModels.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tag")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "blue",
                            Tag = 4,
                            Text = "Go to gym",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Color = "red",
                            Tag = 3,
                            Text = "Wash the dishes",
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Color = "green",
                            Tag = 2,
                            Text = "Do the homework",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("SEDC.NotesApp.Models.DbModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "john123",
                            UserName = "John_Doe"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Jane",
                            LastName = "Doe",
                            Password = "jane123",
                            UserName = "Jane_Doe"
                        });
                });

            modelBuilder.Entity("SEDC.NotesApp.Models.DbModels.Note", b =>
                {
                    b.HasOne("SEDC.NotesApp.Models.DbModels.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SEDC.NotesApp.Models.DbModels.User", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
