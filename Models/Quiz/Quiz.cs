using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace quizmoon.Models
{
    public class Quiz
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Quiz name is required")]
        [MinLength(3, ErrorMessage = "Quiz name cannot be shorter than 3 characters")]
        [MaxLength(20, ErrorMessage = "Quiz name cannot be longer than 20 characters")]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "Quiz category cannot be shorter than 3 characters")]
        [MaxLength(20, ErrorMessage = "Quiz category cannot be longer than 20 characters")]
        public string Category { get; set; }

        public List<QuizQuestion> QuizQuestions { get; set; }

        public byte[] Image { get; set; }
        public string CreatorId { get; set; }

        public object DTO()
        {
            return new
            {
                this.Id,
                this.Name,
                this.Category,
                QuizQuestions = this.QuizQuestions?.Select(x => x.DTO()).ToArray()
            };
        }
    }
}