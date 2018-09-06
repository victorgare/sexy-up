using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.Infrastructure.Context
{
    public class ApplicationDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDatabaseContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDatabaseContext>());
        }

        public static ApplicationDatabaseContext Create()
        {
            return new ApplicationDatabaseContext();
        }

    }
}