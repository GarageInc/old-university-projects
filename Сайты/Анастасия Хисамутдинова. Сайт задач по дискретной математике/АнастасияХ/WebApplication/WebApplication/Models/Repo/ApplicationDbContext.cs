using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<MathTask> Requests { get; set; }
        public DbSet<MathTaskSolution> RequestSolutions { get; set; }
        public DbSet<RecallMessage> RecallMessages { get; set; }
        //public DbSet<ErrorMessage> ErrorMessages { get; set; }

        public DbSet<Document> Documents { get; set; }
        
        public DbSet<Contact> Contacts { get; set; }
        
        public DbSet<Comment> Comments { get; set; }

        public DbSet<StudentGroup> StudentsGroups { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Requests);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.RequestSolutions);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.RecallMessages);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Contacts);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Comments);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.UpComments);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.DownComments);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.UpRecalls);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.DownRecalls);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.SupervisedGroups);


            modelBuilder.Entity<MathTask>().HasMany(c => c.RequestSolutions);
            modelBuilder.Entity<MathTask>().HasMany(c => c.Comments);

            modelBuilder.Entity<StudentGroup>().HasMany(c => c.Students);
            // modelBuilder.Entity<MathTask>().HasMany(c => c.Documents);
            // modelBuilder.Entity<MathTaskSolution>().HasMany(c => c.Documents);
        }
    }
}