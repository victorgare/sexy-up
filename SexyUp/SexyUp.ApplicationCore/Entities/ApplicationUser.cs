using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SexyUp.ApplicationCore.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CpfCnpj { get; set; }
        public string PhantasyName { get; set; }
        public string Site { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentity(UserManager<ApplicationUser> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            manager.AddClaim(Id, new Claim(nameof(FirstName), FirstName));

            return userIdentity;
        }
    }
}