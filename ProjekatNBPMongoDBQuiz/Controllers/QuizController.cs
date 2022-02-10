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

        public async Task<IActionResult> MineQuizzes(bool updateStatus = false, string quizTitle = "")
        {
            if (updateStatus)
                ViewBag.Message = "Uspešno ste izmenili kviz " + quizTitle;
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

        public async Task<IActionResult> UpdateQuiz([FromBody] Quiz quiz)
        {
            await _quizService.UpdateQuizAsync(quiz);
            return RedirectToAction("MineQuizzes", "Quiz", new { updateStatus = true, quizTitle = quiz.Title });
        }
    }
}
