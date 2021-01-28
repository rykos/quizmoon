using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace quizmoon.Models
{
    public class NewQuizDTO
    {
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }
        public IFormFile Avatar { get; set; }
    }
}