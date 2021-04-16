using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DBConfiguration.EFCore
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(DatabaseConnection.ConnectionConfiguration
                                                    .GetConnectionString("DefaultConnection"));
            }
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
           
        }

        public DbSet<User> User { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}
