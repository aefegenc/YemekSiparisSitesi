using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using YemekSiparis.Models;

namespace YemekSiparis.Controllers
{
    public class LoginController : Controller
    {
        string connectionString = "Server=.\\SQLEXPRESS; Database=Haber; Integrated Security=True; TrustServerCertificate=Yes";
        public IActionResult Index()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");

            ViewBag.AuthError = TempData["AuthError"] as string;
            return View();
        }
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["AuthError"] = "Form eksik veya hatalı.";
                return RedirectToAction("Index");
            }
            using var connection = new SqlConnection(connectionString);
            var login = connection.Query<LoginModel>("SELECT * FROM Users").ToList();
            foreach (var user in login)
            {

                if (model.Username == user.Username && model.Password == user.Password)
                {
                    HttpContext.Session.SetString("username", user.Username);
                    ViewData["username"] = HttpContext.Session.GetString("username");

                    return RedirectToAction("Index", "Admin");
                }
            }

            TempData["AuthError"] = "Kullanıcı adı veya şifre hatalı";
            return RedirectToAction("Login");
        }
        public IActionResult Exit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
