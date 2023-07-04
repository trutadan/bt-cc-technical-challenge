using System.ComponentModel.DataAnnotations;

namespace technical_challenge.Model.DTO
{
    public class LoginCredentials
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;
    }
}
