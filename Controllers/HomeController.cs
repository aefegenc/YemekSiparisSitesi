using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YemekSipariş.Models;

namespace YemekSipariş.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
