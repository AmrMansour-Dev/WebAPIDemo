using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
