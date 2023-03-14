using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;

        public DbSet<Hero> Heroes { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Admin> Admins { get; set; } = null!;


        // public DbSet<TodoItemDTO> TodoItemDTOs { get; set; } = null!;

        // public DbSet<HeroDTO> HeroDTOs { get; set; } = null!;
    }
}