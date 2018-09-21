using System.ComponentModel.DataAnnotations;

namespace SexyUp.Web.ViewModels.Order
{
    public class CheckoutViewModel
    {
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Endereço"), Required]
        public string Street { get; set; }

        [Display(Name = "Número/Complemento"), Required]
        public string Number { get; set; }

        [Display(Name = "Cep"), Required]
        public string ZipCode { get; set; }
    }
}