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
    }
}
