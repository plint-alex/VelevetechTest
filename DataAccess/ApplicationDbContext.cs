using DataAccess.Contracts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Student> Students { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Login);


            modelBuilder.Entity<Student>()
                .HasKey(x => new { x.Id });

            modelBuilder.Entity<Student>()
                .Property(x => x.Sex).IsRequired().HasConversion(new EnumToStringConverter<SexEnum>());

            modelBuilder.Entity<Student>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(40);

            modelBuilder.Entity<Student>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(40);

            modelBuilder.Entity<Student>()
                .Property(x => x.MiddleName).HasMaxLength(60);

            modelBuilder.Entity<Student>()
                .Property(x => x.Uid).HasMaxLength(40);


            modelBuilder.Entity<Student>()
                .HasIndex(x => x.Uid)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(x => x.Sex);




        }
    }
}
