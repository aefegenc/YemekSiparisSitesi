using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using YemekSiparis.Models;

namespace YemekSiparis.Controllers
{
    public class CategoryController : Controller
    {
        string connectionString = "Server=.\\SQLEXPRESS; Database=Haber; Integrated Security=True; TrustServerCertificate=Yes";
        public IActionResult Index()
        {
            using var connection = new SqlConnection(connectionString);
            var category = connection.Query<Category>("SELECT * FROM Categories").ToList();

            return View(category);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.MessageCssClass = "alert-danger";
                ViewBag.Message = "Eksik veya hatalı işlem yaptın";
                return View("Message");
            }
            using var connection = new SqlConnection(connectionString);
            var sql = "INSERT INTO Categories (Category) VALUES (@Category)";
            var data = new
            {
                model.Categorys,
            };
            var rowsAffected = connection.Execute(sql, data);
            ViewBag.MessageCssClass = "alert-success";
            ViewBag.Message = "Eklendi.";
            return View("Message");
        }

    }
}
