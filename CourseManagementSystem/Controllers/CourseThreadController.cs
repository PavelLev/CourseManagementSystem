using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Controllers.Forum
{
    public class CourseThreadController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index(int? courseThreadId)
        {
            if (courseThreadId == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var thread = db.CourseThreads.Find(courseThreadId);
            if (thread == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.IsAuthenticated || (User.Identity.GetUserId() != thread.Course.UserId &&
                thread.Course.Enrollments.All(item => User.Identity.GetUserId() != item.UserId)))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(thread);
        }
        
        [HttpPost]
        public ActionResult Create([Bind(Include = "CourseId, Name")] CourseThread thread)
        {
            thread.UserId = User.Identity.GetUserId();
            thread.User = db.Users.Find(thread.UserId);
            thread.LastChangeDateTime = DateTime.Now;
            db.CourseThreads.Add(thread);
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