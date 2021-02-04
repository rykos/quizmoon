using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace quizmoon.Models
{
    public class QuizQuestion
    {
        public long Id { get; set; }
        [MaxLength(50, ErrorMessage = "Quiz question cannot be longer than 50 characters")]
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public List<QuizAnswer> Answers { get; set; }
        public string Type { get; set; } = "Text";
        public string AnswersType { get; set; } = "Text";

        public Quiz Quiz { get; set; }
        public long QuizId { get; set; }

        public object DTO()
        {
            return new
            {
                this.Id,
                this.QuizId,
                this.Text,
                this.Type,
                this.AnswersType,
                Answers = this.Answers?.Select(a => a.DTO()).ToArray()
            };
        }
    }
}