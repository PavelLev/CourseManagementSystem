using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseManagmentSystem.Models;
using Microsoft.AspNet.Identity;

namespace CourseManagmentSystem.Controllers
{
    public class LessonController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Lesson
        public ActionResult Index()
        {
            return View(db.Lessons.ToList());
        }

        // GET: Lesson/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }*/

        public ActionResult Details(int lessonId)
        {
            var lesson = db.Lessons.Find(lessonId);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }
        // GET: Lesson/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lesson/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonID,presentation,text")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Lessons.Add(lesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lesson);
        }

        // GET: Lesson/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lesson/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonID,presentation,text")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lesson);
        }

        // GET: Lesson/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lesson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            db.Lessons.Remove(lesson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Test(int lessonId)
        {
            var lesson = db.Lessons.Find(lessonId);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            var enrollmentId =
                user.Enrollments.First((enrollment) => enrollment.CourseId == lesson.CourseId).EnrollmentID;
            var model = new TestLessonViewModel
            {
                Lesson = lesson,
                LessonID = lesson.LessonID,
                EnrollmentID = enrollmentId,
                Questions = lesson.Questions.Select(question => question.text).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Test()
        {
            var lessonId = int.Parse(Request.Form["LessonID"]);
            var enrollmentId = int.Parse(Request.Form["EnrollmentID"]);
            var answerCount = int.Parse(Request.Form["AnswerCount"]);
            for (var i = 0; i < answerCount; i++)
            {
                var answer = Request.Form["Answer" + i];
                db.QuestionAnswers.Add(new QuestionAnswer
                {
                    LessonID = lessonId,
                    EnrollmentID = enrollmentId,
                    Question = Request.Form["Question" + i],
                    Answer = answer,
                    Mark = answer == "" ? 0 as int? : null
                });
            }
            db.SaveChanges();
            return RedirectToAction("Details", "Lesson", new {lessonId});
        }

        public ActionResult Forum(int lessonId)
        {
            var lesson = db.Courses.Find(lessonId);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
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
