using Microsoft.AspNet.Identity.EntityFramework;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.Infrastructure.Context
{
    public class ApplicationDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDatabaseContext() : base("DefaultConnection")
        {

        }

        public static ApplicationDatabaseContext Create()
        {
            return new ApplicationDatabaseContext();
        }

    }
}