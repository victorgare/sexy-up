using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntityValidation.Attributes;
using EntityValidation.Validation;
using SexyUp.Utils.Utils;

namespace SexyUp.Web.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar este navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : Validate<RegisterViewModel>
    {
        [Display(Name = "Nome"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome"), Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve haver pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare(nameof(Password), ErrorMessage = "As senhas são diferentes")]
        public string ConfirmPassword { get; set; }

        private string _cpf;
        [Required, Cpf]
        public string Cpf
        {
            get => Mask.RemoveMask(_cpf);
            set => _cpf = value;
        }

        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Display(Name = "Data Aniversário")]
        public string BirthDate { get; set; }

        private string _phoneNumber;
        [Display(Name = "Número Telefone")]
        public string PhoneNumber
        {
            get => Mask.RemoveMask(_phoneNumber)?.Replace(" ", string.Empty);
            set => _phoneNumber = value;
        }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve haver pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare(nameof(Password), ErrorMessage = "As senhas são diferentes")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
