using Microsoft.AspNetCore.Http;

namespace quizmoon.Models
{
    public class NewQuestionDTO
    {
        public string Text { get; set; }
        public IFormFile Image { get; set; }
    }
}