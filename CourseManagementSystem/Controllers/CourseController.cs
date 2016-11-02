using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CourseManagementSystem.Models;
using Microsoft.AspNet.Identity;

namespace CourseManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private AppDbContext db = new AppDbContext();

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
        public async Task<ActionResult> Create([Bind(Include = "CourseId,name,description")] Course course)
        {
            course.UserId = User.Identity.GetUserId();
            course.Activated = true;
            if (!ModelState.IsValid)
                return View(course);
            db.Courses.Add(course);
            db.SaveChanges();
            foreach (var user in db.Users.ToList())
            {
                if (user.EmailNotifications)
                {
                    await EmailNotifications.Send(user.Email,
                        "New course",
                        "New course " + course.Name + " has opened. Check it out!");
                }
            }
            return RedirectToAction("Edit", new { id = course.CourseId });
        }

        public async Task<ActionResult> Subscribe(int courseId)
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
            if (course.User == null) return RedirectToAction("Details", new {id = courseId});
            if (course.User.EmailNotifications)
            {
                await EmailNotifications.Send(course.User.Email,
                    "Subscription",
                    "User " + User.Identity.GetUserName() + " has subscribed to your course " + course.Name);
            }

            return RedirectToAction("Details", new {id = courseId});
        }

        public async Task<ActionResult> Unsubscribe(int courseId)
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

            if (course.User == null) return RedirectToAction("Index", "Home");

            if (course.User.EmailNotifications)
            {
                await EmailNotifications.Send(course.User.Email,
                    "Subscription",
                    "User " + User.Identity.GetUserName() + " has unsubscribed to your course " + course.Name);
            }

            return RedirectToAction("Index", "Home");
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
            if (!User.Identity.IsAuthenticated || User.Identity.GetUserId() != course.UserId)
            {
                return View("Details", course);
            }
            return View("LecturerDetails",course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Course editedCourse)
        {
            if (!ModelState.IsValid) return View(editedCourse);
            var course = db.Courses.Find(editedCourse.CourseId);
            course.Name = editedCourse.Name;
            course.Description = editedCourse.Description;
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
            return PartialView("Edit", course);
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
            if (!User.Identity.IsAuthenticated || User.Identity.GetUserId() != course.UserId)
            {
                return View("Details", course);
            }
            return View(course);
        }

        public ActionResult Activation(int id)
        {
            var course = db.Courses.Find(id);
            course.Activated = !course.Activated;
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
            return PartialView("ActivationButton", course);
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
            if (!User.Identity.IsAuthenticated || (User.Identity.GetUserId() != course.UserId &&
                course.Enrollments.All(item => User.Identity.GetUserId() != item.UserId)))
            {
                return RedirectToAction("Index", "Home");
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
            if (!User.Identity.IsAuthenticated || User.Identity.GetUserId() != course.UserId)
            {
                return View("Details", course);
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
