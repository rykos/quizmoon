using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace quizmoon.Models
{
    public class QuizQuestion
    {
        public long Id { get; set; }
        [MaxLength(50, ErrorMessage = "Quiz question cannot be longer than 50 characters")]
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public List<QuizAnswer> Answers { get; set; }
        
        public Quiz Quiz { get; set; }
        public long QuizId { get; set; }
    }
}