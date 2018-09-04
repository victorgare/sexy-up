using System.ComponentModel.DataAnnotations;
using EntityValidation.Attributes;
using EntityValidation.Validation;

namespace SexyUp.Web.ViewModels.FornecedorUser
{
    public class FornecedorUserViewModel : Validate<FornecedorUserViewModel>
    {
        public string Id { get; set; }

        [Display(Name = "Nome"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome"), Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }

        [Required, Cnpj]
        public string Cnpj { get; set; }
    }
}