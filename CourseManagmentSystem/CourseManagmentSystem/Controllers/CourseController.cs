using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseManagmentSystem.Models;
using Microsoft.AspNet.Identity;

namespace CourseManagmentSystem.Controllers
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

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,name,description")] Course course)
        {
            course.UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid) return View(course);
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Subscribe(int courseId)
        {
            var enrollment = new Enrollment {UserId = User.Identity.GetUserId(), CourseId = courseId};
            db.Enrollments.Add(enrollment);
            db.SaveChanges();
            return RedirectToAction("Details",new {id = courseId});

        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View("LecturerDetails",course);
        }

   
        [HttpPost]
        public ActionResult Edit([Bind(Include = "CourseID,name,description")] Course course)
        {
            if (!ModelState.IsValid) PartialView("Edit", course);
            course.UserId = User.Identity.GetUserId();
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
            return PartialView("Edit", course);
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
