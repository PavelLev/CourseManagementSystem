using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Home
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated) return View(db.Courses.ToList());

            var user = db.Users.Find(User.Identity.GetUserId());
            if (user == null)
            {
                return RedirectToAction("LogOut", "Account");
            }
            return View(db.Courses.ToList());
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