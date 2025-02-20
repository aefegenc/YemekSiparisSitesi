using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.IO;
using YemekSiparis.Models;

namespace YemekSiparis.Controllers
{
    public class AdminController : Controller
    {
        string connectionString = "Server=.\\SQLEXPRESS; Database=Haber; Integrated Security=True; TrustServerCertificate=Yes";

        public IActionResult Index()
        {
            using var connection = new SqlConnection(connectionString);
            var menus = connection.Query<Admin>("SELECT Filmler.*, Kategori FROM Filmler LEFT JOIN Kategoriler ON Filmler.KategoriId = Kategoriler.Id ORDER BY Kategoriler.Kategori ASC").ToList();
            return View(menus);
        }
    }
}
