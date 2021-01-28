using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace quizmoon.Models
{
    public class NewAnswerDTO
    {
        public string Text { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public bool Correct { get; set; }
    }
}