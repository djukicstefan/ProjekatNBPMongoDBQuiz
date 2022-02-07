using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjekatNBPMongoDBQuiz.Extensions;
using ProjekatNBPMongoDBQuiz.IServices;
using ProjekatNBPMongoDBQuiz.Models;
using ProjekatNBPMongoDBQuiz.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService us) { this._userService = us; } 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> TryRegister(string username, string email, string city, string phone, string password)
        {
            if (HttpContext.Session.IsLoggedIn())
                return RedirectToAction("Index", "Home");

            await _userService.AddUserAsync(new User
            {
                Username = username,
                Password = password,
                Phone = phone,
                Email = email,
                City = city
            });

            User user = await _userService.GetUserAsync(username, password);

            if (user != null)
            {
                HttpContext.Session.SetString(SessionKeys.Username, user.Username);
                HttpContext.Session.SetString(SessionKeys.UserId, user.Id);
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Register", "Home");            
        }

        public async Task<IActionResult> TryLogin(string username, string password)
        {
            if (HttpContext.Session.IsLoggedIn())
                return RedirectToAction("Login", "Home");

            User user = await _userService.GetUserAsync(username, password);

            if(user != null)
            {
                HttpContext.Session.SetString(SessionKeys.Username, user.Username);
                HttpContext.Session.SetString(SessionKeys.UserId, user.Id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Register", "Home");
            }
        }

        public IActionResult Logout()
        {
            if (!HttpContext.Session.IsLoggedIn())
                return RedirectToAction("Login", "Home");

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
