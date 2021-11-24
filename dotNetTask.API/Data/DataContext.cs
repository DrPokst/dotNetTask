using System;
using dotNetTask.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotNetTask.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
