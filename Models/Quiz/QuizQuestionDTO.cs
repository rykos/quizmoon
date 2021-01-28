using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using quizmoon.Helpers;

namespace quizmoon.Models
{
    public class QuizQuestionDTO
    {
        [MaxLength(50, ErrorMessage = "Quiz question cannot be longer than 50 characters")]
        public string Text { get; set; }

        public IFormFile Image { get; set; }
        public QuizAnswerDTO[] Answers { get; set; }

        public QuizQuestion ToQuizQuestion(Quiz quiz)
        {
            QuizQuestion qq = new QuizQuestion()
            {
                Quiz = quiz,
                Text = this.Text,
                Image = SysHelper.FileToByteArray(this.Image)
            };
            qq.Answers = this.Answers.Select(qa => qa.ToQuizAnswer(qq)).ToList();
            return qq;
        }
    }
}