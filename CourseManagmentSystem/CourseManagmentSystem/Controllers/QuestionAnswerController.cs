using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseManagmentSystem.Models;

namespace CourseManagmentSystem.Controllers
{
    public class QuestionAnswerController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Check(int idQuestionAnswer)
        {
            var forum = db.QuestionAnswers.Find(idQuestionAnswer);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        [HttpPost]
        public void Check(QuestionAnswer questionAnswer)
        {
            db.Entry(questionAnswer).State = EntityState.Modified;
            db.SaveChanges();
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