using System.ComponentModel.DataAnnotations;

namespace quizmoon.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(15, ErrorMessage = "Maximum username length is 15 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [MaxLength(40, ErrorMessage = "Maximum email length is 40 characters")]
        [EmailAddress(ErrorMessage = "Is not a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [MinLength(6, ErrorMessage = "Minium password length is 6 characters")]
        [MaxLength(30, ErrorMessage = "Maximum password length is 30 characters")]
        public string Password { get; set; }
    }
}