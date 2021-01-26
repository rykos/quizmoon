using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quizmoon.Data;
using quizmoon.Helpers;
using quizmoon.Models;

namespace quizmoon.Controllers
{
    [ApiController]
    [Route("quiz")]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        public QuizController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("new")]
        public void NewQuiz([FromForm] QuizDTO quiz)
        {
            ApplicationUser user = this.dbContext.Users.FirstOrDefault(u => u.Id == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user == default)
                return;

            Quiz q = new Quiz()
            {
                CreatorId = user.Id,
                Name = quiz.Name,
                Category = quiz.Category
            };
            q.QuizQuestions = quiz.QuizQuestions.Select(qq => qq.ToQuizQuestion(q)).ToList();
            q.Image = SysHelper.FileToByteArray(quiz.Avatar);
            this.dbContext.Quizzes.Add(q);
            this.dbContext.SaveChanges();
        }
    }
}