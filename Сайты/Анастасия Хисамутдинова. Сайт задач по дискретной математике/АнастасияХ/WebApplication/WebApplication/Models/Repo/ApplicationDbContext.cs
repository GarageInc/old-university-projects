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
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestSolution> RequestSolutions { get; set; }
        public DbSet<RecallMessage> RecallMessages { get; set; }
        //public DbSet<ErrorMessage> ErrorMessages { get; set; }

        public DbSet<Document> Documents { get; set; }
        
        public DbSet<Contact> Contacts { get; set; }
        
        public DbSet<Comment> Comments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Requests);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.RequestSolutions);
            //modelBuilder.Entity<ApplicationUser>().HasMany(c => c.ErrorMessages);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.RecallMessages);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Contacts);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Comments);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.UpComments);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.DownComments);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.UpRecalls);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.DownRecalls);


            modelBuilder.Entity<Request>().HasMany(c => c.RequestSolutions);
            modelBuilder.Entity<Request>().HasMany(c => c.Comments);
            
            //modelBuilder.Entity<Request>().HasMany(c => c.Documents);
            //modelBuilder.Entity<RequestSolution>().HasMany(c => c.Documents);
        }
    }
}