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

        public IActionResult Register(bool passfail = false, bool exist = false)
        {
            if(exist)
            {
                ViewBag.Username = TempData["Username"] as string;
                ViewBag.Email = TempData["Email"] as string;
                ViewBag.City = TempData["City"] as string;
                ViewBag.Phone = TempData["Phone"] as string;
                ViewBag.UMessage = "Postoji korisnik sa datim korisničkim imenom u bazi!";
                return View();
            }
            else if(passfail)
            {
                ViewBag.Username = TempData["Username"] as string;
                ViewBag.Email = TempData["Email"] as string;
                ViewBag.City = TempData["City"] as string;
                ViewBag.Phone = TempData["Phone"] as string;
                ViewBag.PMessage = "Lozinke se ne poklapaju!";                
            }
            return View();
        }

        public IActionResult Login(bool exist = true)
        {
            if (!exist)
                ViewBag.MyMessage = "Ne postoji korisnik sa datim korisničkim imenom i lozinkom u bazi";
            return View();
        }

        public async Task<IActionResult> TryRegister(string username, string email, string city, string phone, string password, string repassword)
        {
            if (HttpContext.Session.IsLoggedIn())
                return RedirectToAction("Index", "Home");

            if(password.CompareTo(repassword) != 0)
            {
                TempData["Username"] = username;
                TempData["Email"] = email;
                TempData["City"] = city;
                TempData["Phone"] = phone;
                return RedirectToAction("Register", "User", new { passfail = true });
            }

            var listUsers = await _userService.GetUsersAsync();
            if(listUsers.Any(x => x.Username == username))
            {
                TempData["Username"] = username;
                TempData["Email"] = email;
                TempData["City"] = city;
                TempData["Phone"] = phone;
                return RedirectToAction("Register", "User", new { exist = true });
            }

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
                return RedirectToAction("Register", "User");            
        }

        public async Task<IActionResult> TryLogin(string username, string password)
        {
            if (HttpContext.Session.IsLoggedIn())
                return RedirectToAction("Index", "Home");

            User user = await _userService.GetUserAsync(username, password);

            if(user != null)
            {
                HttpContext.Session.SetString(SessionKeys.Username, user.Username);
                HttpContext.Session.SetString(SessionKeys.UserId, user.Id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "User", new { exist = false }) ;
            }
        }

        public IActionResult Logout()
        {
            if (!HttpContext.Session.IsLoggedIn())
                return RedirectToAction("Login", "User");

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Profile(bool passfailed = false)
        {            
            var userId = HttpContext.Session.GetUserId();
            var user = await _userService.GetUserByIdAsync(userId);

            if (passfailed)
                ViewBag.Message = "Neuspešna izmena lozinke zbog nepoklapanja!";

            return View(user);                       
        }

        public async Task<IActionResult> ChangeData(string id, string username, string email, string city, string phone, string password1, string password, string repassword)
        {
            var newPass = password1;
            if (!string.IsNullOrEmpty(password) && password.CompareTo(repassword) == 0)
                newPass = password;

            await _userService.UpdateUserAsync(new User
            {
                Id = id,
                Username = username,
                Password = newPass,
                Phone = phone,
                Email = email,
                City = city
            });

            if (!string.IsNullOrEmpty(password) && password.CompareTo(repassword) != 0)
                return RedirectToAction("Profile", "User", new { passfailed = true });

            return RedirectToAction("Profile", "User");
        }
    }
}
