using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models.Identity.AccountViewModels
{
    public class RegisterViewModel
    {
        #region Public Properties

        public bool AcceptTerms { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public bool AllowMarketing { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool Active { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime DateEdit { get; set; }
       
        #endregion Public Properties
    }
}