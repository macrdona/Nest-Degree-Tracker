﻿using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompletedCourses>()
                .HasKey(k => new { k.UserId, k.CourseId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<EnrollmentForm> Enrollments { get; set; }
        public DbSet<Majors> Majors { get; set; }

        public DbSet<CompletedCourses> CompletedCourses { get;set; }
    }
}
