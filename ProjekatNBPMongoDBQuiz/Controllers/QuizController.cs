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
        private readonly ILeaderboardService _leaderboardService;

        public QuizController(IQuizService qs, IUserService us, ILeaderboardService ls)
        {
            _quizService = qs;            
            _userService = us;
            _leaderboardService = ls;
        }
        
        public IActionResult Index() => View();

        public async Task<IActionResult> CreateQuiz([FromBody] Quiz quiz)
        {
            var userId = HttpContext.Session.GetUserId();            
            
            quiz.UserId = userId;

            await _quizService.AddQuizAsync(quiz);
            await _leaderboardService.AddLeaderboardAsync(new Leaderboard() { 
                Id = quiz.Id, 
                Content = new List<LeaderboardItem>() 
            });

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MineQuizzes(bool changed = false)
        {            
            if (changed)
                ViewBag.Message = "Uspešno ste ažurirali kviz! ";
            
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
            await _leaderboardService.DeleteLeaderboardAsync(quizId);
            await _quizService.DeleteQuizAsync(quizId);
            return RedirectToAction("MineQuizzes", "Quiz");
        }

        public async Task<IActionResult> UpdateQuiz([FromBody] Quiz quiz)
        {
            await _quizService.UpdateQuizAsync(quiz);
            return RedirectToAction("MineQuizzes", "Quiz");
        }

        public async Task<IActionResult> ChangeQuiz(string quizId)
        {
            var quiz = await _quizService.GetQuizByIdAsync(quizId);
            return View(quiz);
        }

        public async Task<IActionResult> QuizPreview(string quizId)
        {
            var quiz = await _quizService.GetQuizByIdAsync(quizId);
            var leaderboard = await _leaderboardService.GetLeaderboardByIdAsync(quizId);

            ViewBag.QuizTitle = quiz.Title;
            ViewBag.QuizId = quiz.Id;

            return View(leaderboard);
        }

        public async Task<IActionResult> StartQuiz(string quizId)
        {
            var quiz = await _quizService.GetQuizByIdAsync(quizId);

            return View(quiz);
        }

        public async Task SubmitQuiz([FromBody] Quiz quiz)
        {
            var userId = HttpContext.Session.GetUserId();
            var q = await _quizService.GetQuizByIdAsync(quiz.Id);
            var leaderboard = await _leaderboardService.GetLeaderboardByIdAsync(quiz.Id);

            int correctAnswers = quiz.Validate(q);

            long time = long.Parse(quiz.Title);

            var leaderboardItem = leaderboard.Content.Find(x => x.User.Id == userId);
            if (leaderboardItem != null)
            {
                if (correctAnswers > leaderboardItem.Score)
                {
                    leaderboardItem.Score = correctAnswers;
                    leaderboardItem.Time = time;
                }
                else if (correctAnswers == leaderboardItem.Score && time < leaderboardItem.Time)
                    leaderboardItem.Time = time;
            }
            else
            {
                leaderboard.Content.Add(new LeaderboardItem() { 
                    User = await _userService.GetUserByIdAsync(userId),
                    Score = correctAnswers,
                    Time = time
                });
            }
            
            ViewBag.Message = $"Uspešno ste završili kviz sa {correctAnswers} od {quiz.Questions.Count} tačnih odgovora!";
            
            await _leaderboardService.UpdateLeaderboardAsync(leaderboard);
        }
    }
}
