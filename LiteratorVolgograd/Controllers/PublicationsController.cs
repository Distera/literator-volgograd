using System.Diagnostics;
using System.Linq;
using LiteratorVolgograd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiteratorVolgograd.Controllers
{
    public class PublicationsController : Controller
    {
        const string DefoltTitle = "Без названия";
        private readonly ApplicationContext db;

        public PublicationsController(ApplicationContext context)
        {
            db = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(db.Publications.OrderBy(s => s.Title));
        }

        public IActionResult ManagePublication(int? id)
        {
            return View(id == null ? null : db.Publications.FirstOrDefault(n => n.Id == id));
        }

        [HttpPost]
        public IActionResult AddPublication(string title, string content)
        {
            db.Publications.Add(new Publication
            {
                Title = title ?? DefoltTitle,
                Content = content,
            });
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdatePublication(int id, string title, string content)
        {
            var publication = db.Publications.Find(id);

            publication.Title = title ?? DefoltTitle;
            publication.Content = content;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult ViewPublication(int id)
        {
            var publication = db.Publications.FirstOrDefault(p => p.Id == id);
            if (publication != null)
                return View(publication);

            return NotFound();
        }

        [HttpPost]
        public IActionResult DeletePublication(int id)
        {
            var publication = db.Publications.Find(id);
            if (publication != null)
            {
                db.Publications.Remove(publication);
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