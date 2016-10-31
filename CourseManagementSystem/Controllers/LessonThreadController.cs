using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseManagementSystem.Models;
using Microsoft.AspNet.Identity;

namespace CourseManagementSystem.Controllers
{
    public class LessonThreadController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index(int? courseThreadId)
        {
            if (courseThreadId == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var thread = db.LessonThreads.Find(courseThreadId);
            if (thread == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.IsAuthenticated || (User.Identity.GetUserId() != thread.Lesson.Course.UserId &&
                thread.Lesson.Course.Enrollments.All(item => User.Identity.GetUserId() != item.UserId)))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(thread);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "LessonId, Name")] LessonThread thread)
        {
            thread.UserId = User.Identity.GetUserId();
            thread.User = db.Users.Find(thread.UserId);
            thread.LastChangeDateTime = DateTime.Now;
            db.LessonThreads.Add(thread);
            db.SaveChanges();
            return PartialView("Bar", thread);
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