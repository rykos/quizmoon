using System.ComponentModel.DataAnnotations;

namespace quizmoon.Models
{
    public class QuizAnswer
    {
        public long Id { get; set; }

        [MaxLength(50, ErrorMessage = "Quiz answer text cannot be longer than 50 characters")]
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public bool Correct { get; set; }

        public QuizQuestion QuizQuestion { get; set; }
        public long QuizQuestionId { get; set; }
    }
}