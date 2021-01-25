using System.ComponentModel.DataAnnotations;

namespace quizmoon.Models
{
    public class AdminRegisterModel : RegisterModel
    {
        [Required(ErrorMessage = "missing secret")]
        [MaxLength(50, ErrorMessage = "secret cannot be longer than 50 characters")]
        public string Key { get; set; }
    }
}