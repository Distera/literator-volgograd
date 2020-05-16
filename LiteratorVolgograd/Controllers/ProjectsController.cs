using System;
using System.Diagnostics;
using System.Linq;
using LiteratorVolgograd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiteratorVolgograd.Controllers
{
    public class ProjectsController : Controller
    {
        const string DefoltTitle = "Без названия";
        private readonly ApplicationContext db;

        public ProjectsController(ApplicationContext context)
        {
	        db = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(db.Projects.OrderByDescending(s => s.Date));
        }

        public IActionResult ManageProject(int? id)
        {
            return View(id == null ? null : db.Projects.FirstOrDefault(n => n.Id == id));
        }

        [HttpPost]
        public IActionResult AddProject(string title, string content)
        {
            db.Projects.Add(new Project
            {
                Title = title ?? DefoltTitle,
                Date = DateTime.Now,
                Content = content,
            });
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateProject(int id, string title, string content)
        {
            var projects = db.Projects.Find(id);
            projects.Title = title ?? DefoltTitle;
            projects.Content = content;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult ViewProject(int id)
        {
            var projects = db.Projects.FirstOrDefault(p => p.Id == id);
            if (projects != null)
                return View(projects);

            return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteProject(int id)
        {
            var projects = db.Projects.Find(id);
            if (projects != null)
            {
                db.Projects.Remove(projects);
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