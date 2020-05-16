using System;
using System.Diagnostics;
using System.Linq;
using LiteratorVolgograd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteratorVolgograd.Controllers
{
    public class NewsController : Controller
    {
        const string DefoltTitle = "Без названия";
        private readonly ApplicationContext db;

        public NewsController(ApplicationContext context)
        {
	        db = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(db.News.OrderByDescending(s => s.Date));
        }

        public IActionResult ManageNews(int? id)
        {
            return View(id == null ? null : db.News.FirstOrDefault(n => n.Id == id));
        }

        [HttpPost]
        public IActionResult AddNews(string title, string content)
        {
            db.News.Add(new News
            {
                Title = title ?? DefoltTitle,
                Date = DateTime.Now,
                Content = content,
            });
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateNews(int id, string title, string content)
        {
            var news = db.News.Find(id);

            news.Title = title ?? DefoltTitle;
            news.Content = content;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult ViewNews(int id)
        {
            var news = db.News.FirstOrDefault(p => p.Id == id);
            if (news != null)
                return View(news);

            return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteNews(int id)
        {
            var news = db.News.Find(id);
            if (news != null)
            {
                db.News.Remove(news);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}