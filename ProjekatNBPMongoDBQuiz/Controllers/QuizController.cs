using Microsoft.AspNetCore.Mvc;
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

        public QuizController(IQuizService qs) => _quizService = qs;
        
        public IActionResult Index() => View();

        public IActionResult CreateQuiz(string title, string type)
        {
            //_quizService.AddQuizAsync(new Quiz()
            //{
            //    AuthorId = authorId,
            //    Type = type,
            //    Description = description,
            //    Questions = questions.ToList()
            //});

            return RedirectToAction("Index", "Home");
        }
    }
}
