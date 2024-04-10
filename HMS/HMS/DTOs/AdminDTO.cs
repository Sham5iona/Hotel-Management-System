using System.ComponentModel.DataAnnotations;

namespace HMS.DTOs
{
    public class AdminDTO
    {
        [Display(Name = "username")]
        [Required(ErrorMessage = "Please, enter a username!")]
        public string Username { get; set; }

        [Display(Name = "password")]
        [Required(ErrorMessage = "Please, enter a password!"),
            MinLength(5, ErrorMessage = "Password must be at least 5 chars!")]
        public string Password { get; set; }

        public string? AlreadyExists { get; set; }
    }
}
