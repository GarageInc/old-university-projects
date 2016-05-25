using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HelpDeskTrain.Models
{
    public class HelpdeskContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Lifecycle> Lifecycles { get; set; }
        public DbSet<Hardware> Hardwares { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Orgtechnic> Orgtechnics { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Activ> Activs { get; set; }
    }
}