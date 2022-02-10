using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjekatNBPMongoDBQuiz.Extensions;
using ProjekatNBPMongoDBQuiz.IServices;
using ProjekatNBPMongoDBQuiz.Models;
using ProjekatNBPMongoDBQuiz.Session;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQuizService _quizService;
        private readonly IUserService _userService;
        private readonly ILeaderboardService _leaderboardService;

        public HomeController(ILogger<HomeController> logger, IQuizService quizService, IUserService userService, ILeaderboardService leaderboardService)
        {
            _logger = logger;
            _quizService = quizService;
            _userService = userService;
            _leaderboardService = leaderboardService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return View();
            else
            {
                var quizList = await _quizService.GetNotUserQuizzesAsync(userId);
                if (quizList != null)
                {
                    List<UserQuiz> userQuizzes = new List<UserQuiz>();
                    quizList.ForEach(async quiz =>
                    {
                        User user = await _userService.GetUserByIdAsync(quiz.UserId);
                        userQuizzes.Add(new UserQuiz { Quiz = quiz, Username = user.Username });
                    });
                    return View(userQuizzes);
                }
                else
                    return View();
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GlobalLeaderboard()
        {
            var leaderboards = await _leaderboardService.GetLeaderboardsAsync();
            var globalLeaderboard = new Leaderboard()
            {
                Content = new List<LeaderboardItem>()
            };

            foreach (var l in leaderboards)
            {
                foreach (var c in l.Content)
                {
                    var content = globalLeaderboard.Content.Find(x => x.User.Id == c.User.Id);
                    if (content != null)
                    {
                        content.Score += c.Score;
                        content.Time += c.Time;
                    }
                    else
                    {
                        globalLeaderboard.Content.Add(new LeaderboardItem()
                        {
                            Score = c.Score,
                            Time = c.Time,
                            User = c.User
                        });
                    }
                }
            }

            return View(globalLeaderboard);
        }
    }
}
