using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace quizmoon.Models
{
    public class QuizDTO
    {
        [Required(ErrorMessage = "Quiz name is required")]
        [MinLength(3, ErrorMessage = "Quiz name cannot be shorter than 3 characters")]
        [MaxLength(20, ErrorMessage = "Quiz name cannot be longer than 20 characters")]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "Quiz category cannot be shorter than 3 characters")]
        [MaxLength(20, ErrorMessage = "Quiz category cannot be longer than 20 characters")]
        public string Category { get; set; }

        public IFormFile Avatar { get; set; }

        [Required]
        [MinLength(1)]
        public List<QuizQuestionDTO> QuizQuestions { get; set; }
    }
}