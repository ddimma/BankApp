using System.ComponentModel.DataAnnotations;

namespace BankApp.Shared
{
    public class RegisterDto
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [UsernameValidation]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$",
            ErrorMessage = "Password should have minimum 8 characters, at least 1 uppercase letter, 1 lowercase letter and 1 number.")]
        public string Password { get; set; }
    }
}

