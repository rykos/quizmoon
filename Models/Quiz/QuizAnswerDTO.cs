using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;
using quizmoon.Helpers;

namespace quizmoon.Models
{
    public class QuizAnswerDTO
    {
        [MaxLength(50, ErrorMessage = "Quiz answer text cannot be longer than 50 characters")]
        public string Text { get; set; }
        public IFormFile Image { get; set; }
        public bool Correct { get; set; }

        public QuizAnswer ToQuizAnswer(QuizQuestion quizQuestion)
        {
            return new QuizAnswer()
            {
                Correct = this.Correct,
                QuizQuestion = quizQuestion,
                Text = this.Text,
                Image = SysHelper.FileToByteArray(this.Image)
            };
        }
    }
}