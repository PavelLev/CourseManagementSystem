using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CourseManagmentSystem.Models;

namespace CourseManagmentSystem.Controllers.Forum
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