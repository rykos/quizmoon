using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quizmoon.Data;
using quizmoon.Helpers;
using quizmoon.Models;
using System;
using Microsoft.EntityFrameworkCore;

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

        // [HttpPost]
        // [Route("new")]
        // public void NewQuiz([FromForm] QuizDTO quiz)
        // {
        //     ApplicationUser user = this.dbContext.Users.FirstOrDefault(u => u.Id == User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //     if (user == default)
        //         return;

        //     Quiz q = new Quiz()
        //     {
        //         CreatorId = user.Id,
        //         Name = quiz.Name,
        //         Category = quiz.Category
        //     };
        //     q.QuizQuestions = quiz.QuizQuestions.Select(qq => qq.ToQuizQuestion(q)).ToList();
        //     q.Image = SysHelper.FileToByteArray(quiz.Avatar);
        //     this.dbContext.Quizzes.Add(q);
        //     this.dbContext.SaveChanges();
        // }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateQuiz([FromForm] NewQuizDTO newQuizDTO)
        {
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();

            Quiz quiz = new Quiz()
            {
                CreatorId = user.Id,
                Name = newQuizDTO.Name,
                Category = newQuizDTO.Category
            };
            this.dbContext.Quizzes.Add(quiz);
            this.dbContext.SaveChanges();

            return Ok(quiz);
        }

        [HttpPost]
        [Route("create/question/{quizId}")]
        public IActionResult CreateQuestion(long quizId, [FromForm] NewQuestionDTO newQuestion)
        {
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            Quiz quiz = this.GetQuiz(quizId);
            if (quiz.CreatorId != user.Id)
                return Unauthorized(ResponseDTO.Error("Dont even try to modify other people quizzes"));

            QuizQuestion qq = new QuizQuestion()
            {
                QuizId = quizId,
                Text = newQuestion.Text ?? ""
            };

            this.dbContext.QuizQuestions.Add(qq);
            this.dbContext.SaveChanges();

            return Ok(qq);
        }

        [HttpPost]
        [Route("create/answer/{questionId}")]
        public IActionResult CreateAnswer(long questionId, [FromForm] NewAnswerDTO answerDTO)
        {
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            long quizId = this.dbContext.QuizQuestions.Select(qq => new { qq.Id, qq.QuizId }).FirstOrDefault(qq => qq.Id == questionId).QuizId;
            Quiz quiz = this.GetQuiz(quizId);
            if (quiz.CreatorId != user.Id)
                return Unauthorized(ResponseDTO.Error("Dont even try to modify other people quizzes"));

            QuizAnswer qa = new()
            {
                QuizQuestionId = questionId,
                Text = answerDTO.Text ?? "",
                Correct = answerDTO.Correct
            };

            this.dbContext.QuizAnswers.Add(qa);
            this.dbContext.SaveChanges();

            return Ok(qa);
        }

        [HttpGet]
        [Route("{quizId}")]
        public IActionResult QuizById(long quizId)
        {
            Quiz quiz = this.dbContext.Quizzes.FirstOrDefault(q => q.Id == quizId);
            if (quiz == default)
                return NotFound();
            quiz.QuizQuestions = this.dbContext.QuizQuestions?.Where(qq => qq.QuizId == quizId)?.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

            return Ok(quiz.DTO());
        }

        [HttpGet]
        [Route("question/{questionId}")]
        public IActionResult QuestionById(long questionId)
        {
            QuizQuestion question = this.dbContext.QuizQuestions.Include(qq => qq.Answers).FirstOrDefault(qq => qq.Id == questionId);
            if (question == default)
                return NotFound();

            return Ok(question.DTO());
        }

        private ApplicationUser GetUser()
        {
            ApplicationUser user = this.dbContext.Users.FirstOrDefault(u => u.Id == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return user;
        }

        private Quiz GetQuiz(long quizId)
        {
            return this.dbContext.Quizzes.FirstOrDefault(q => q.Id == quizId);
        }
    }
}