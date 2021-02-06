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
using System.Collections.Generic;

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

        #region Create
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
                Category = newQuizDTO.Category,
                Image = SysHelper.FileToByteArray(newQuizDTO.Avatar)
            };
            this.dbContext.Quizzes.Add(quiz);
            this.dbContext.SaveChanges();

            return Ok(quiz.DTO());
        }

        [HttpPost]
        [Route("create/question/{quizId}")]
        public IActionResult CreateQuestion(long quizId, [FromForm] NewQuestionDTO newQuestion)
        {
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            Quiz quiz = this.GetQuiz(quizId);
            if (quiz == default)
                return NotFound();
            if (quiz.CreatorId != user.Id)
                return Unauthorized(ResponseDTO.Error("Dont even try to modify other people quizzes"));

            QuizQuestion qq = new QuizQuestion()
            {
                QuizId = quizId,
                Text = newQuestion.Text ?? "",
                Image = SysHelper.FileToByteArray(newQuestion.Image)
            };

            this.dbContext.QuizQuestions.Add(qq);
            this.dbContext.SaveChanges();

            return Ok(qq.DTO());
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
            if (quiz == default)
                return NotFound();
            if (quiz.CreatorId != user.Id)
                return Unauthorized(ResponseDTO.Error("Dont even try to modify other people quizzes"));

            QuizAnswer qa = new()
            {
                QuizQuestionId = questionId,
                Text = answerDTO.Text ?? "",
                Correct = answerDTO.Correct,
                Image = SysHelper.FileToByteArray(answerDTO.Image)
            };

            this.dbContext.QuizAnswers.Add(qa);
            this.dbContext.SaveChanges();

            return Ok(qa.DTO());
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateQuiz([FromForm] NewQuizDTO newQuizDTO)
        {
            if (newQuizDTO.Id == default)
                return BadRequest(ResponseDTO.Error("missing id attribute"));
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            Quiz quiz = this.GetQuiz(newQuizDTO.Id);
            if (quiz == default)
                return NotFound();

            quiz.CreatorId = user.Id;
            quiz.Name = newQuizDTO.Name;
            quiz.Category = newQuizDTO.Category;
            quiz.Public = newQuizDTO.Public;
            if (newQuizDTO.Avatar != default)
                quiz.Image = SysHelper.FileToByteArray(newQuizDTO.Avatar);

            this.dbContext.Quizzes.Update(quiz);
            this.dbContext.SaveChanges();

            return Ok(quiz.DTO());
        }

        [HttpPut]
        [Route("update/question")]
        public IActionResult UpdateQuestion([FromForm] NewQuestionDTO newQuestion)
        {
            if (newQuestion.Id == default)
                return BadRequest(ResponseDTO.Error("missing id attribute"));
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            QuizQuestion quizQuestion = this.dbContext.QuizQuestions.FirstOrDefault(qq => qq.Id == newQuestion.Id);
            Quiz quiz = (quizQuestion != default) ? this.GetQuiz(quizQuestion.QuizId) : null;
            if (quiz == default || quizQuestion == default)
                return NotFound();
            if (quiz.CreatorId != user.Id)
                return Unauthorized(ResponseDTO.Error("Dont even try to modify other people quizzes"));

            quizQuestion.Text = newQuestion.Text ?? "";
            quizQuestion.Type = newQuestion.Type;
            quizQuestion.AnswersType = newQuestion.AnswersType;
            if (newQuestion.Image != default)
                quizQuestion.Image = SysHelper.FileToByteArray(newQuestion.Image);

            this.dbContext.QuizQuestions.Update(quizQuestion);
            this.dbContext.SaveChanges();

            return Ok(quizQuestion.DTO());
        }

        [HttpPut]
        [Route("update/answer/{questionId}")]
        public IActionResult UpdateAnswer(long questionId, [FromForm] NewAnswerDTO answerDTO)
        {
            if (answerDTO.Id == default)
                return BadRequest(ResponseDTO.Error("missing id attribute"));
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            QuizAnswer qa = this.dbContext.QuizAnswers.FirstOrDefault(qa => qa.Id == answerDTO.Id);
            if (qa == default)
                return NotFound();
            long quizId = this.dbContext.QuizQuestions.Select(qq => new { qq.Id, qq.QuizId }).FirstOrDefault(qq => qq.Id == questionId)?.QuizId ?? 0;
            Quiz quiz = this.GetQuiz(quizId);
            if (quiz == default || qa == default)
                return NotFound();
            if (quiz.CreatorId != user.Id)
                return Unauthorized(ResponseDTO.Error("Dont even try to modify other people quizzes"));

            qa.Text = answerDTO.Text ?? "";
            qa.Correct = answerDTO.Correct;
            if (answerDTO.Image != default)
                qa.Image = SysHelper.FileToByteArray(answerDTO.Image);

            this.dbContext.QuizAnswers.Update(qa);
            this.dbContext.SaveChanges();

            return Ok(qa.DTO());
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("delete/quiz/{quizId}")]
        public IActionResult DeleteQuiz(long quizId)
        {
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            Quiz quiz = this.GetQuiz(quizId);
            if (quiz == default)
                return NotFound();
            if (quiz.CreatorId != user.Id)
                return BadRequest();

            this.dbContext.Quizzes.Remove(quiz);
            this.dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("delete/question/{questionId}")]
        public IActionResult DeleteQuestion(long questionId)
        {
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            QuizQuestion question = this.dbContext.QuizQuestions.FirstOrDefault(q => q.Id == questionId);
            if (question == default)
                return NotFound();
            Quiz quiz = this.GetQuiz(question.Id);
            if (quiz.CreatorId != user.Id)
                return BadRequest();

            this.dbContext.QuizQuestions.Remove(question);
            this.dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("delete/answer/{answerId}")]
        public IActionResult DeleteAnswer(long answerId)
        {
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            QuizAnswer answer = this.dbContext.QuizAnswers.FirstOrDefault(qa => qa.Id == answerId);
            if (answer == default)
                return NotFound();
            string CreatorId = this.dbContext.QuizAnswers.Include(qa => qa.QuizQuestion).ThenInclude(qq => qq.Quiz).Select(qa => new { qa.QuizQuestion.Quiz.CreatorId, qa.Id })
                .FirstOrDefault(qa => qa.Id == answerId).CreatorId;
            if (user.Id != CreatorId)
                return BadRequest();

            this.dbContext.QuizAnswers.Remove(answer);
            this.dbContext.SaveChanges();

            return Ok();
        }

        #endregion

        [HttpGet]
        [Route("my")]
        public IActionResult MyQuizzes()
        {
            ApplicationUser user = this.GetUser();
            if (user == default)
                return Unauthorized();
            object[] quizzes = this.dbContext.Quizzes.Select(q => new { q.CreatorId, q.Id, q.Name })
                .Where(q => q.CreatorId == user.Id).Select(q => new { q.Id, q.Name }).ToArray();
            return Ok(quizzes);
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

        [HttpGet]
        [Route("top")]
        public IActionResult GetQuizzes([FromQuery] int skip = 0)
        {
            object[] quizzes = this.dbContext.Quizzes.Where(q => q.Public)?.OrderBy(q => q.Id).Skip(skip)?.Take(20)?.Select(q => q.DTO()).ToArray();
            return Ok(quizzes);
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