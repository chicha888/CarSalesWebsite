using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarSalesWebsite.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "First name is required!")]
        [StringLength(40, ErrorMessage = "First name cannot be longer than 50 symbols")]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "Last name cannot be longer than 50 symbols")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Enter a valid phone number (+380XXXXXXXXX)")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
