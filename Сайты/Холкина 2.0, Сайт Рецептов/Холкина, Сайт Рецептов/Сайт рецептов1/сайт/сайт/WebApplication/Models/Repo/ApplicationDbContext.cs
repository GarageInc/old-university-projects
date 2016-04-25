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
        public DbSet<Article> Articles { get; set; }
        public DbSet<RecallMessage> RecallMessages { get; set; }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Ingridient> Ingridients { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Articles);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.RecallMessages);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Comments);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.UpComments);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.DownComments);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.UpRecalls);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.DownRecalls);

            modelBuilder.Entity<Article>().HasMany(c => c.Comments);

            modelBuilder.Entity<Category>().HasMany(c => c.Ingridients);

            // Связь многие со многим. Так как один рецепт может иметь много ингридиентов, присутсвующих в других рецептах
            modelBuilder.Entity<Article>().HasMany(c => c.Ingridients);
            modelBuilder.Entity<Article>().HasMany(c => c.Ingridients)
                    .WithMany(s => s.Articles)
                    .Map(t => t.MapLeftKey("ArticleId")
                    .MapRightKey("IngridientId")
                    .ToTable("ArticleIngridient"));

            //modelBuilder.Entity<Article>().HasMany(c => c.Documents);
            //modelBuilder.Entity<ArticleSolution>().HasMany(c => c.Documents);
        }
    }
}