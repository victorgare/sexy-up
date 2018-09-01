using System.ComponentModel.DataAnnotations;

namespace SexyUp.Web.ViewModels.AdminUser
{
    public class AdminUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nome"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome"), Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }
    }
}