using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteratorVolgograd.Models;
using Microsoft.EntityFrameworkCore;

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
            ViewData["Message"] = "*Устав, правление и т.д.*";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "*Контакты: почта, телефон и т.д.*";

            return View();
        }

        public IActionResult Authors()
        {
            ViewData["Message"] = "*Список авторов*";

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
