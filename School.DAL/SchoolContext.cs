using Microsoft.EntityFrameworkCore;
using School.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Salutation> Salutations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().HasIndex(t => t.SalutationId).IsUnique(false);
            modelBuilder.Entity<Class>().HasIndex(c => c.TeacherId).IsUnique(false);
            modelBuilder.Entity<Student>().HasIndex(s => s.ClassId).IsUnique(false);

            modelBuilder.Entity<Salutation>().HasData(
                new Salutation { SalutationId = 1, Type = "Mr", CreatedDate = DateTime.UtcNow },
                new Salutation { SalutationId = 2, Type = "Mrs", CreatedDate = DateTime.UtcNow },
                new Salutation { SalutationId = 3, Type = "Ms", CreatedDate = DateTime.UtcNow },
                new Salutation { SalutationId = 4, Type = "Miss", CreatedDate = DateTime.UtcNow },
                new Salutation { SalutationId = 5, Type = "Dr", CreatedDate = DateTime.UtcNow }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                    .UseSqlServer("Server=tcp:mbqitschool.database.windows.net,1433;Initial Catalog=SchoolDB;Persist Security Info=False;User ID=mbadmin;Password=Test@9876;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", options => options.EnableRetryOnFailure());
        }
    }
}
