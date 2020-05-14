using System.Collections.Generic;
using System.Linq;
using LiteratorVolgograd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace LiteratorVolgograd.Controllers
{
    public class AuthorsController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        public IActionResult Index()
        {
            return View(db.Authors.OrderBy(s => s.LastName));
        }

        public IActionResult ManageAuthor(int? id)
        {
            return View(id == null ? null : db.Authors.FirstOrDefault(n => n.Id == id));
        }

        public IActionResult ViewAuthor(int id)
        {
            var authors = db.Authors.FirstOrDefault(p => p.Id == id);
            if (authors != null)
                return View(authors);

            return NotFound();
        }

        public IActionResult AddAuthor(string firstName, string lastName, string middleName, DateTime dateOfBirth, string about)
        {
            db.Authors.Add(new Author
            {
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                DateOfBirth = dateOfBirth,
                About = about
            });
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateAuthor(int id, string firstName, string lastName, string middleName, DateTime dateOfBirth, string about)
        {
            var author = db.Authors.Find(id);
            author.FirstName = firstName;
            author.LastName = lastName;
            author.MiddleName = middleName;
            author.DateOfBirth = dateOfBirth;
            author.About = about;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAuthor(int id)
        {
            var author = db.Authors.Find(id);
            if (author != null)
            {
                db.Authors.Remove(author);
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