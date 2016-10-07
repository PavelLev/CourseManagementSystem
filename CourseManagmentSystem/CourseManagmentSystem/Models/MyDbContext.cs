using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CourseManagmentSystem.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("CourseManagementSystem")
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}