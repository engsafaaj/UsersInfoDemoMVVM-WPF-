using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersInfo.Core;

namespace UsersInfo.Data
{
    public class DBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=UserDemoDB;Encrypt=False;Integrated Security=True");
        }

        // Add Tables

        public DbSet<User> Users { get; set; }
    }
}
