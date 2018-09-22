using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.Infrastructure.Context
{
    public class ApplicationDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionItens> TransactionItens { get; set; }

        public ApplicationDatabaseContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDatabaseContext>());
            Configuration.LazyLoadingEnabled = true;
        }

        public static ApplicationDatabaseContext Create()
        {
            return new ApplicationDatabaseContext();
        }

    }
}