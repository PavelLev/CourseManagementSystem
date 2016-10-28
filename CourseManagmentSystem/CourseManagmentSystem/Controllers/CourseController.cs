using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseManagmentSystem.Migrations;
using CourseManagmentSystem.Models;
using Microsoft.AspNet.Identity;

namespace CourseManagmentSystem.Controllers
{
    public class CourseController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Course
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,name,description")] Course course)
        {
            course.UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
                return View(course);
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = course.CourseId });
        }

        public ActionResult Subscribe(int courseId)
        {
            var course = db.Courses.Find(courseId);
            var userId = User.Identity.GetUserId();
            if (course.UserId == userId)
            {
                return RedirectToAction("Edit", "Course", new { id = courseId });
            }
            if (course.Enrollments.Any((item) => item.UserId == userId))
            {
                return RedirectToAction("Details", "Course", new {id = courseId});
            }
            var enrollment = new Enrollment {UserId = User.Identity.GetUserId(), CourseId = courseId};
            db.Enrollments.Add(enrollment);
            db.SaveChanges();
            return RedirectToAction("Details", new {id = courseId});
        }

        public ActionResult UnSubscribe(int courseId)
        {
            var course = db.Courses.Find(courseId);
            var userId = User.Identity.GetUserId();
            if (course.UserId == userId)
            {
                return RedirectToAction("Edit", "Course", new { id = courseId });
            }


            var enrollment = course.Enrollments.FirstOrDefault((item) => item.UserId == userId);

            if (enrollment == null)
            {
                return RedirectToAction("Details", "Course", new { id = courseId });
            }

            db.Enrollments.Remove(enrollment);

            db.SaveChanges();
            return RedirectToAction("Details", new { id = courseId });
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,name,description")] Course course)
        {
            if (!ModelState.IsValid) return View(course);
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details");
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Forum(int courseId)
        {
            var course = db.Courses.Find(courseId);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Check(int courseId)
        {
            var course = db.Courses.Find(courseId);
            if (course == null)
            {
                return HttpNotFound();
            }
            var model = new CheckCourseAnswersViewModel
            {
                Course = course,
                Answers = new List<QuestionAnswer>()
            };

            foreach (var enrollment in course.Enrollments)
            {
                foreach (var answer in enrollment.QuestionAnswers)
                {
                    if (answer.Mark == null)
                    {
                        model.Answers.Add(answer);
                    }
                }
            }

            return View(model);
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
