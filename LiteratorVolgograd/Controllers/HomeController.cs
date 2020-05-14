using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LiteratorVolgograd.Models;

namespace LiteratorVolgograd.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        public IActionResult Index()
        {
            ViewData["Message"] = "*Последние посты из всех разделов*";

            return View();
        }

        public IActionResult About()
        {
            if (db.About.Count() == 0)
            {
                db.About.Add(new About { Content = ""});
                db.SaveChanges();
            }

            return View(db.About.First());
        }

        public IActionResult ManageAbout()
        {
            return View(db.About.First());
        }

        [HttpPost]
        public IActionResult UpdateAbout(string content)
        {
            var about = db.About.First();
            about.Content = content;
            db.SaveChanges();

            return RedirectToAction("About");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "*Контакты: почта, телефон и т.д.*";

            return View();
        }

        public IActionResult Projects()
        {
            ViewData["Message"] = "*Список проектов*";

            return View();
        }

        public IActionResult Publications()
        {
            ViewData["Message"] = "*Список публикаций*";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
