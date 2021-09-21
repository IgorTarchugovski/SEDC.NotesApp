using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Models
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(
                entity =>
                {
                    entity.HasKey(entity => entity.Id);
                    entity
                        .Property(entity => entity.UserName)
                        .IsRequired()
                        .HasMaxLength(100);
                    entity
                        .Property(e => e.Password)
                        .IsRequired()
                        .HasMaxLength(20);

                    entity.HasData(
                        new User()
                        {
                            Id = 1,
                            UserName = "John_Doe",
                            Password = "john123",
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new User()
                        {
                            Id = 2,
                            UserName = "Jane_Doe",
                            Password = "jane123",
                            FirstName = "Jane",
                            LastName = "Doe"
                        }
                    );
                }
            );

            modelBuilder.Entity<Note>(
                entity =>
                {
                    entity.HasKey(entity => entity.Id);
                    entity.Property(e => e.Text).IsRequired();

                    entity.HasOne(e => e.User)
                        .WithMany(entity => entity.Notes)
                        .HasForeignKey(entity => entity.UserId);

                    entity.HasData(
                        new Note()
                        {
                            Id = 1,
                            Text = "Go to gym",
                            Color = "blue",
                            Tag = 4,
                            UserId = 1
                        },
                        new Note()
                        {
                            Id = 2,
                            Text = "Wash the dishes",
                            Color = "red",
                            Tag = 3,
                            UserId = 2
                        },
                        new Note()
                        {
                            Id = 3,
                            Text = "Do the homework",
                            Color = "green",
                            Tag = 2,
                            UserId = 1
                        }
                    );
                }
            );
        }

    }
}
