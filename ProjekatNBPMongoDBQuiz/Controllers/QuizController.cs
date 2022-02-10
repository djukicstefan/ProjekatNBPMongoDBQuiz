using Microsoft.AspNetCore.Mvc;
using ProjekatNBPMongoDBQuiz.Extensions;
using ProjekatNBPMongoDBQuiz.IServices;
using ProjekatNBPMongoDBQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;        
        private readonly IUserService _userService;

        public QuizController(IQuizService qs, IUserService us)
        {
            _quizService = qs;            
            _userService = us;
        }
        
        public IActionResult Index() => View();

        public async Task<IActionResult> CreateQuiz([FromBody] Quiz quiz)
        {
            var userId = HttpContext.Session.GetUserId();            
            
            quiz.UserId = userId;
            await _quizService.AddQuizAsync(quiz);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MineQuizzes()
        {            
            List<Quiz> UserQuizzes = null;
            var userId = HttpContext.Session.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                UserQuizzes = await _quizService.GetUserQuizzes(userId);
            }

            return View(UserQuizzes);
        }

        public async Task<IActionResult> DeleteQuiz(string quizId)
        {
            await _quizService.DeleteQuizAsync(quizId);
            return RedirectToAction("MineQuizzes", "Quiz");
        }

        public async Task<IActionResult> ChangeQuiz(string quizId)
        {
            var quiz = await _quizService.GetQuizByIdAsync(quizId);
            return View(quiz);
        }

        public async Task<IActionResult> UpdateAnswer(string quizId, string questionText, string oldContent, string aCheckbox, string aContent)
        {
            if(oldContent.CompareTo(aContent) != 0)
            {
                bool correct = string.IsNullOrEmpty(aCheckbox) ? false : true;
                var quiz = await _quizService.GetQuizByIdAsync(quizId);
                var question = quiz.Questions.Find(qu => qu.Text == questionText);
                var answer = question.Answers.Find(ans => ans.Text == oldContent);
                answer.Text = aContent;
                answer.Correct = correct;
                await _quizService.UpdateQuizAsync(quiz);                                                               
            }

            return RedirectToAction("ChangeQuiz", "Quiz", new { quizId = quizId });
        }

        public async Task<IActionResult> UpdateQuestion(string quizId, string qTextOld, string qText)
        {
            var quiz = await _quizService.GetQuizByIdAsync(quizId);
            var question = quiz.Questions.Find(qu => qu.Text == qTextOld);
            question.Text = qText;
            await _quizService.UpdateQuizAsync(quiz);

            return RedirectToAction("ChangeQuiz", "Quiz", new { quizId = quizId });
        }

        public async Task<IActionResult> UpdateQuizInfo(string quizId, string quizTitle, string quizCategory)
        {
            var quiz = await _quizService.GetQuizByIdAsync(quizId);
            quiz.Title = quizTitle;
            quiz.Category = quizCategory;
            await _quizService.UpdateQuizAsync(quiz);

            return RedirectToAction("ChangeQuiz", "Quiz", new { quizId = quizId });
        }
    }
}
