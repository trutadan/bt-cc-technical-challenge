using System.ComponentModel.DataAnnotations;

namespace technical_challenge.Model.DTO
{
    public class RegisterCredentials
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Password must be between 5 and 20 characters.", MinimumLength = 5)]
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
