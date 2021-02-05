using System.Linq;
using Microsoft.AspNetCore.Mvc;
using quizmoon.Data;

namespace quizmoon.Controllers
{
    [ApiController]
    [Route("image")]
    public class ImageController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        public ImageController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("quiz/avatar/{quizId}")]
        public IActionResult GetQuizAvatar(long quizId)
        {
            byte[] imgBuffor = this.dbContext.Quizzes.Select(q => new { q.Id, q.Image }).FirstOrDefault(q => q.Id == quizId)?.Image;
            if (imgBuffor == default)
                return NotFound();
            return File(imgBuffor, "image/jpeg");
        }

        [HttpGet]
        [Route("quiz/question/{questionId}")]
        public IActionResult GetQuestionImage(long questionId)
        {
            byte[] imgBuffor = this.dbContext.QuizQuestions.Select(qq => new { qq.Id, qq.Image }).FirstOrDefault(q => q.Id == questionId)?.Image;
            if (imgBuffor == default)
                return NotFound();
            return File(imgBuffor, "image/jpeg");
        }

        [HttpGet]
        [Route("quiz/answer/{answerId}")]
        public IActionResult GetAnswerImage(long answerId)
        {
            byte[] imgBuffor = this.dbContext.QuizAnswers.Select(qa => new { qa.Id, qa.Image }).FirstOrDefault(q => q.Id == answerId)?.Image;
            if (imgBuffor == default)
                return NotFound();
            return File(imgBuffor, "image/jpeg");
        }
    }
}