using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Controllers
{
    public class QuestionController : Controller
    {
        private AppDbContext db = new AppDbContext();

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (!ModelState.IsValid) return PartialView("Create", question);
            db.Questions.Add(question);
            db.SaveChanges();
            return PartialView("Edit", question);
        }

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (!ModelState.IsValid) return PartialView("Edit", question);
            db.Entry(question).State = EntityState.Modified;
            db.SaveChanges();
            return PartialView("Edit", question);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int questionId)
        {
            var question = db.Questions.Find(questionId);
            db.Questions.Remove(question);
            db.SaveChanges();
            return new EmptyResult();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}