using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LiteratorVolgograd.Models;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace LiteratorVolgograd.Controllers
{
    public class HomeController : Controller
    {
        const int ProjectRecordsCount = 4;
        const int NewsRecordsCount = 3;
        const int AuthorRecordsCount = 5;
        const int PublicationRecordsCount = 3;
        private readonly ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
	        db = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var viewMain = new ViewMain()
            {
                Project = db.Projects.OrderByDescending(x => x.Date).Take(ProjectRecordsCount).ToList(),
                News = db.News.OrderByDescending(x => x.Date).Take(NewsRecordsCount).ToList(),
                Author = db.Authors.OrderBy(x => Guid.NewGuid()).Take(AuthorRecordsCount).ToList(),
                Publication = db.Publications.OrderBy(x => Guid.NewGuid()).Take(PublicationRecordsCount).ToList()
            };

            return View(viewMain);
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["Message"] = "*Контакты: почта, телефон и т.д.*";

            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
