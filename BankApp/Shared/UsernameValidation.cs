using System.ComponentModel.DataAnnotations;

namespace BankApp.Shared
{
    class UsernameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!value.ToString().ToLower().Contains("admin"))
            {
                return null;
            }

            return new ValidationResult("The UserName cannot contain the word admin",
                new[] { validationContext.MemberName });
        }
    }
}

