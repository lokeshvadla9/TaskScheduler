using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskScheduler.Repository
{
    public  class TasksDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoItemHistory> TodoItemHistories { get; set; }

        public TasksDbContext(DbContextOptions<TasksDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<TodoItem>()
                .Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasMany(u => u.TodoItems)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<TodoItem>()
                .HasMany(t => t.TodoItemHistories)
                .WithOne(th => th.TodoItem)
                .HasForeignKey(th => th.TodoItemId);
        }
    }
}
