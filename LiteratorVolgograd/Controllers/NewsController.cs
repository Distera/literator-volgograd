using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LiteratorVolgograd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteratorVolgograd.Controllers
{
    public class NewsController : Controller
    {
        const string DefoltTitle = "Без названия";
        private ApplicationContext db = new ApplicationContext();

        public IActionResult Index()
        {
            IEnumerable<News> news = db.News;
            ViewBag.News = news.OrderByDescending(s => s.Date);

            return View();
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
            News news = db.News.Find(id);

            news.Title = title ?? DefoltTitle;
            news.Content = content;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ViewNews(int id)
        {
            News news = db.News.FirstOrDefault(p => p.Id == id);
            if (news != null)
                return View(news);

            return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteNews(int id)
        {
            News news = db.News.Find(id);
            if (news != null)
            {
                db.News.Remove(news);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}