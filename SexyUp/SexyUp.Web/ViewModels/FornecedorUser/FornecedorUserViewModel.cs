using System;
using System.ComponentModel.DataAnnotations;
using EntityValidation.Attributes;
using EntityValidation.Validation;
using SexyUp.Utils.Utils;

namespace SexyUp.Web.ViewModels.FornecedorUser
{
    public class FornecedorUserViewModel : Validate<FornecedorUserViewModel>
    {
        public string Id { get; set; }

        [Display(Name = "Nome"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome"), Required]
        public string LastName { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        private string _cnpj;
        [Required, Cnpj]
        public string Cnpj
        {
            get => Mask.RemoveMask(_cnpj);
            set => _cnpj = value;
        }

        private string _phoneNumber;
        [Phone]
        public string PhoneNumber
        {
            get => Mask.RemoveMask(_phoneNumber)?.Replace(" ", string.Empty);
            set => _phoneNumber = value;
        }

        public string PhantasyName { get; set; }

        [Url]
        public string Site { get; set; }
    }
}