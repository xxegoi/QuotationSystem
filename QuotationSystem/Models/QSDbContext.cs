using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuotationSystem.Models
{
    public class QSDbContext:DbContext
    {
        public QSDbContext() : base("QS") { }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}